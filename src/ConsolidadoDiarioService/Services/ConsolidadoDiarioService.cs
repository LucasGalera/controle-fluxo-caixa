using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ConsolidadoDiarioService.Data;
using ConsolidadoDiarioService.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace ConsolidadoDiarioService.Services
{
    public class ConsolidadoDiarioService : IConsolidadoDiarioService
    {
        private readonly ConsolidadoDiarioDbContext _context;
        private readonly IConnection _rabbitConnection;
        private readonly IModel _channel;

        public ConsolidadoDiarioService(
            ConsolidadoDiarioDbContext context,
            IConnection rabbitConnection)
        {
            _context = context;
            _rabbitConnection = rabbitConnection;
            _channel = _rabbitConnection.CreateModel();
            ConfigureRabbitMQ();
        }

        private void ConfigureRabbitMQ()
        {
            _channel.QueueDeclare("lancamentos", durable: true, exclusive: false, autoDelete: false);
            
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var lancamento = JsonSerializer.Deserialize<dynamic>(message);
                
                await ProcessarLancamentoAsync(lancamento);
            };

            _channel.BasicConsume(queue: "lancamentos",
                                autoAck: true,
                                consumer: consumer);
        }

        public async Task<ConsolidadoDiario> GetByDateAsync(DateTime data)
        {
            return await _context.ConsolidadosDiarios
                .Include(c => c.Movimentos)
                .FirstOrDefaultAsync(c => c.Data.Date == data.Date);
        }

        public async Task<IEnumerable<ConsolidadoDiario>> GetByPeriodAsync(DateTime inicio, DateTime fim)
        {
            return await _context.ConsolidadosDiarios
                .Include(c => c.Movimentos)
                .Where(c => c.Data.Date >= inicio.Date && c.Data.Date <= fim.Date)
                .OrderBy(c => c.Data)
                .ToListAsync();
        }

        public async Task<ConsolidadoDiario> ProcessarConsolidadoDiarioAsync(DateTime data)
        {
            var consolidado = await _context.ConsolidadosDiarios
                .Include(c => c.Movimentos)
                .FirstOrDefaultAsync(c => c.Data.Date == data.Date);

            if (consolidado == null)
            {
                consolidado = new ConsolidadoDiario
                {
                    Data = data.Date,
                    Movimentos = new List<MovimentoDiario>()
                };
                _context.ConsolidadosDiarios.Add(consolidado);
            }

            // Aqui seria implementada a lógica de consolidação dos lançamentos do dia
            // Consultando a API de Lançamentos ou processando a fila do RabbitMQ

            await _context.SaveChangesAsync();
            return consolidado;
        }

        private async Task ProcessarLancamentoAsync(dynamic lancamento)
        {
            var data = DateTime.Parse(lancamento.Data.ToString()).Date;
            var consolidado = await GetByDateAsync(data);

            if (consolidado == null)
            {
                consolidado = new ConsolidadoDiario
                {
                    Data = data,
                    Movimentos = new List<MovimentoDiario>()
                };
                _context.ConsolidadosDiarios.Add(consolidado);
            }

            // Atualizar totais e movimentos
            if (lancamento.Tipo.ToString() == "Credito")
            {
                consolidado.TotalCreditos += lancamento.Valor;
            }
            else
            {
                consolidado.TotalDebitos += lancamento.Valor;
            }

            consolidado.SaldoDiario = consolidado.TotalCreditos - consolidado.TotalDebitos;

            await _context.SaveChangesAsync();
        }
    }
}