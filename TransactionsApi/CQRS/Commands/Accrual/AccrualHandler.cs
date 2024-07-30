using MediatR;
using TransactionsApi.Interface;
using TransactionsApi.Models;

namespace TransactionsApi.CQRS.Commands.Accrual
{
    public class AccrualHandler : IRequestHandler<AccrualCommand, AccrualResult>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IClientRepository _clientRepository;

        public AccrualHandler(ITransactionRepository transactionRepository,
            IClientRepository clientRepository)
        {
            _transactionRepository = transactionRepository;
            _clientRepository = clientRepository;
        }

        public async Task<AccrualResult> Handle(AccrualCommand request,
            CancellationToken cancellationToken)
        {
            if(!_clientRepository.ClientExists(request.ClientId))
            {
                throw new InvalidOperationException("Unable to perform accrual. Client not found.");
            }

            var accrual = new Transaction
            {
                Amount = request.Amount,
                ClientId = request.ClientId
            };

            await _transactionRepository.CreateTransactionAsync(accrual);

            return new AccrualResult { Message = "Successfull accrual." };
        }
    }
}
