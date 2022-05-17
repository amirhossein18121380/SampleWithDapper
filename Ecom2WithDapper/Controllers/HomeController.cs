using DataAccess.Dal;
using DataModel.Models;
using DataModel.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Ecom2WithDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IProductCategoryDal _productCategoryDal;
        private readonly IProductDal _productDal;
        private readonly IProductReceiptDal _productReceiptDal;
        private readonly IProductSaleFactorDal _productSaleFactorDal;
        private readonly IReceiptDal _receiptDal;
        private readonly ISaleFactorDal _saleFactorDal;

        public HomeController(IProductCategoryDal productCategoryDal, IProductDal productDal,
            IProductReceiptDal productReceiptDal, IProductSaleFactorDal productSaleFactorDal,
            IReceiptDal receiptDal, ISaleFactorDal saleFactorDal)
        {
            _productCategoryDal = productCategoryDal;
            _productDal = productDal;
            _productReceiptDal = productReceiptDal;
            _productSaleFactorDal = productSaleFactorDal;
            _receiptDal = receiptDal;
            _saleFactorDal = saleFactorDal;
        }

        [HttpPost]
        [Route("AddProduct")]
        public async Task<ActionResult<string>> AddProduct(AddProductViewModel pro)
        {
            try
            {
                var pr = new Product()
                {
                    Name = pro.Name,
                    Price = pro.Price,
                    Code = 0,
                    Description = pro.Description,
                    Createon = DateTime.Now,
                };

                var res = await _productDal.Insert(pr);
                if (res == 0)
                {
                    return "not added";
                }

                pr.Id = res;
                pr.Code = res;

                var es = await _productDal.Update(pr);
                if (es == 0)
                {
                    return "not updated";
                }


                foreach (var i in pro.categoryIds)
                {
                    var ca = new ProductCategory()
                    {
                        CategoryId = i,
                        ProductId = res
                    };

                    var dd = await _productCategoryDal.Insert(ca);
                    if (dd == 0)
                    {
                        return "_productCategoryDal not added";
                    }
                }

                return "Success";
            }
            catch (Exception ex)
            {
                return $@"{ex}";
            }
        }

        [HttpPost]
        [Route("RecordReceipt")]
        public async Task<ActionResult<string>> RecordReceipt(List<long> productids)
        {
            try
            {
                decimal totacCost = 0;
                foreach (var prid in productids)
                {
                    var dd = await _productDal.GetById(prid);

                    totacCost += dd.Price;
                }

                var rece = new Receipt()
                {
                    TotalCost = totacCost,
                    ReceiptNumber = 0,
                    ReceiptDate = DateTime.Now
                };

                var re = await _receiptDal.Insert(rece);

                if (re == 0)
                {
                    return "something wrong with receipt";
                }

                rece.Id = re;
                rece.ReceiptNumber = re;

                var es = await _receiptDal.Update(rece);
                if (es == 0)
                {
                    return "not updated";
                }


                foreach (var i in productids)
                {
                    var ca = new ProductReceipt()
                    {
                        ReceiptId = re,
                        ProductId = i
                    };

                    var dd = await _productReceiptDal.Insert(ca);
                    if (dd == 0)
                    {
                        return "_productReceiptDal not added";
                    }
                }

                return "Success";
            }
            catch (Exception ex)
            {
                return $@"{ex}";
            }
        }

        [HttpPost]
        [Route("RecordSaleFactore")]
        public async Task<ActionResult<string>> RecordSaleFactore(List<long> productids)
        {
            try
            {
                decimal totacCost = 0;
                foreach (var prid in productids)
                {
                    var dd = await _productDal.GetById(prid);

                    totacCost += dd.Price;
                }

                var sale = new SaleFactor()
                {
                    TotalCost = totacCost,
                    ReceiptNumber = 0,
                    ReceiptDate = DateTime.Now
                };

                var re = await _saleFactorDal.Insert(sale);

                if (re == 0)
                {
                    return "something wrong with saleFactor";
                }

                sale.Id = re;
                sale.ReceiptNumber = re;

                var es = await _saleFactorDal.Update(sale);
                if (es == 0)
                {
                    return "not updated";
                }


                foreach (var i in productids)
                {
                    var ca = new ProductSaleFactor()
                    {
                        SaleFactorId = re,
                        ProductId = i
                    };

                    var dd = await _productSaleFactorDal.Insert(ca);
                    if (dd == 0)
                    {
                        return "_productSaleFactorDal not added";
                    }
                }

                return "Success";
            }
            catch (Exception ex)
            {
                return $@"{ex}";
            }
        }
    }
}