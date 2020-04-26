using System.Collections.Immutable;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.CreateScale;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Application.UseCases.Catalog.Scales
{
    public class CreateScale : ValidatedUseCase<CreateScaleInput, ICreateScaleOutputPort>, ICreateScaleUseCase
    {
        private readonly ScaleService _scaleService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateScale(
            ICreateScaleOutputPort outputPort,
            ScaleService scaleService,
            IUnitOfWork unitOfWork)
            : base(new CreateScaleInputValidator(), outputPort)
        {
            _scaleService = scaleService;
            _unitOfWork = unitOfWork;
        }

        protected override async Task Handle(CreateScaleInput input)
        {
            string name = input.Name!;
            var slug = Slug.Of(name);

            bool scaleExists = await _scaleService.ScaleAlreadyExists(slug);
            if (scaleExists)
            {
                OutputPort.ScaleAlreadyExists($"Scale '{name}' already exists");
                return;
            }

            var scaleGauge = input.Gauge.ToScaleGauge();

            var _ = await _scaleService.CreateScale(
                name,
                Ratio.Of(input.Ratio ?? 0M),
                scaleGauge,
                input.Description,
                ImmutableHashSet<ScaleStandard>.Empty,
                input.Weight);

            await _unitOfWork.SaveAsync();

            BuildOutput(slug);
        }

        private void BuildOutput(Slug slug)
        {
            var output = new CreateScaleOutput(slug);
            OutputPort.Standard(output);
        }
    }
}