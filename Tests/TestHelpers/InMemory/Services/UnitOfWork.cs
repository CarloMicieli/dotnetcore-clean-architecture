using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Common.Data;

namespace TreniniDotNet.TestHelpers.InMemory.Services
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private bool _saved = false;

        public async Task<int> SaveAsync()
        {
            _saved = true;
            return await Task.FromResult<int>(0);
        }

        public void EnsureUnitOfWorkWasSaved()
        {
            _saved.Should().BeTrue("IUnitOfWork.SaveAsync was not called.");
        }
    }
}
