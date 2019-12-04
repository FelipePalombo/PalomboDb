using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Servico.Implementacao;
using Servico.Interface;

namespace Background
{
    public class Checkpoint : BackgroundService
    {        
        IServiceScopeFactory _scopeFactory;
        
        public Checkpoint(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var _checkpointServico = scope.ServiceProvider.GetRequiredService<ICheckpointServico>();

                System.Threading.Thread.Sleep(30000);
                while (!stoppingToken.IsCancellationRequested)
                {
                    _checkpointServico.Check(false);
                    System.Threading.Thread.Sleep(30000);
                }
            }
        }

        public override async Task StartAsync(CancellationToken stoppingToken)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var _checkpointServico = scope.ServiceProvider.GetRequiredService<ICheckpointServico>();
                _checkpointServico.SetUndoRedo();
                _checkpointServico.Check(true);
            }
        }
    }
}
           