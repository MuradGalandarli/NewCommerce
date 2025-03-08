using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Application.Features.Commands.ProductImageFile.ChangeShowcaseImage
{
    public class ChangeShowcaseImageCommandHandler : IRequestHandler<ChangeShowcaseImageCommandRequest, ChangeShowcaseImageCommandResponse>
    {
        readonly IProductImageWriteRepository _productImageFileWriteRepository;
        
        public ChangeShowcaseImageCommandHandler(IProductImageWriteRepository productImageFileWriteRepository)
        {
            _productImageFileWriteRepository = productImageFileWriteRepository;
        }

        public async Task<ChangeShowcaseImageCommandResponse> Handle(ChangeShowcaseImageCommandRequest request, CancellationToken cancellationToken)
        {
            var query = _productImageFileWriteRepository.Table
                      .Include(p => p.Products)
                      .SelectMany(p => p.Products, (pif, p) => new
                      {
                          pif,
                          p
                      }).ToList();

            var data =  query.FirstOrDefault(p => p.p.Id == Guid.Parse(request.ProductId) && p.pif.Showcase);

            if (data != null)
                data.pif.Showcase = false;

            var image =  query.FirstOrDefault(p => p.pif.Id == Guid.Parse(request.ImageId));
            if (image != null)
                image.pif.Showcase = true;

            await _productImageFileWriteRepository.SaveAsync();

            return new();
        }
    }
}
