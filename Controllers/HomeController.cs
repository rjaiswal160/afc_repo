using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using AFC.Common;
using AFC.Models;
using Afc_web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace AFC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SqlHelper _sqlHelper;

        public HomeController(ILogger<HomeController> logger, SqlHelper sqlHelper)
        {
            _logger = logger;
            _sqlHelper = sqlHelper;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("oem")]
        public IActionResult Oem()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        [Route("all-standing-desk")]
        [Route("whitestone-workstation")]
        [Route("radiology-office-use")]
        [Route("deluxe-ergo-tier")]
        [Route("ergo-tier")]
        [Route("dual-tier-desks")]
        [Route("single-tier")]
        [Route("radiology-home")] 
        [Route("cardiology")]
        public IActionResult StandingDesk()
        {
            string id = HttpContext.Request.Path.Value?.Trim('/');
            if (id == "radiology-home-use") id = "radiology-home";
            ProductDeskTypeViewModel model = GetProductByDeskTypeId(id);
            return View("StandingDesk", model);
        }

        public ProductDeskTypeViewModel GetProductByDeskTypeId(string id)
        {
            string proc = "SP_GetProductByDeksTypeId";
            SqlParameter[] parameters = new SqlParameter[]
              {
                   new SqlParameter("@DeskTypeId", SqlDbType.NVarChar) { Value = id }
              };


            var (deskTypes, products) = _sqlHelper.ExecuteProcedureWithMultipleResults(proc, parameters);

            ProductDeskTypeViewModel model = new ProductDeskTypeViewModel();

            List<DeskTypeModel> deskTypeList = new List<DeskTypeModel>();
            foreach (DataRow row in deskTypes.Rows)
            {
                var deskType = new DeskTypeModel
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = Convert.ToString(row["Name"]),
                    SEO_Slug = Convert.ToString(row["SEO_Slug"]),
                    ImageUrl = Convert.ToString(row["ImageUrl"]),
                };

                deskTypeList.Add(deskType);
            }
            model.deskTypeModels = deskTypeList;

            List<ProductDeskTypeModel> productDeskTypes = new List<ProductDeskTypeModel>();

            foreach (DataRow row in products.Rows)
            {
                var productDeskType = new ProductDeskTypeModel
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = Convert.ToString(row["Name"]),
                    SEO_Slug = Convert.ToString(row["SEO_Slug"]),
                    ImageUrl = Convert.ToString(row["ImageUrl"]),
                    SKU = Convert.ToString(row["SKU"]),
                    DeskTypeSlug = Convert.ToString(row["DeskTypeSlug"]),
                    DeskTypeName = Convert.ToString(row["DeskTypeName"]),
                    DeskTypeImageUrl = Convert.ToString(row["DeskTypeImageUrl"]),
                };

                productDeskTypes.Add(productDeskType);
            }
            model.productDeskTypeModels = productDeskTypes;
            return model;

        }
        [HttpGet("computer-carts")]
        public IActionResult ComputerCarts()
        {
            return View("ComputerCarts");
        }

        [HttpGet]
        [Route("all-medical-carts")]
        [Route("battery-power-carts")]
        [Route("point-of-care-pc-cart")]
        [Route("telemedicine-cart")]
        [Route("telehealth-pole-cart")]
        [Route("mobile-detector-holder")]
        [Route("telescopic-cart")]
        [Route("lab-cart")]
        [Route("radiology-coat-hanger")]
        public IActionResult Cart()
        {
            string id = HttpContext.Request.Path.Value?.Trim('/');
            ProductDeskTypeViewModel model = GetProductByDeskTypeId(id);
            return View("Cart", model);
        }

        [HttpGet("ergonomic-mounts")]
        public IActionResult ErgonomicMounts()
        {
            return View("ErgonomicMounts");
        }

        [HttpGet]
        [Route("all-ergonomic-mounts")]
        [Route("desktop-monitor-arms")]
        [Route("wall-mounted-monitor-arms")]
        [Route("computer-wall-mounts")]
        [Route("wall-mounted-workstation")]
        [Route("floor-mount-stand")]
        [Route("ceiling-mounted-monitor-arms")]
        public IActionResult Mounts()
        {
            string id = HttpContext.Request.Path.Value?.Trim('/');
            ProductDeskTypeViewModel model = GetProductByDeskTypeId(id);
            return View("Mounts", model);
        }

        [HttpGet("covid-products")]
        public IActionResult CovidProducts(string id)
        {
            List<ProductModel> products = GetProductData(id);
            return View("CovidProducts");
        }



        [HttpGet("product-category/{id}")]
        public IActionResult ProductCategory(string id)
        {
            List<ProductCategoryModel> productCategories = GetProductCategoryData(id);
            return View("ProductCategory", productCategories);
        }

        public List<ProductCategoryModel> GetProductCategoryData(string id)
        {
            string proc = "SP_GetProductByCategoryId";
            List<ProductCategoryModel> productCategories = new List<ProductCategoryModel>();
            SqlParameter[] parameters = new SqlParameter[]
              {
                   new SqlParameter("@CategoryId", SqlDbType.NVarChar) { Value = id }
              };

            DataTable dt = _sqlHelper.ExecuteProcdure(proc, parameters); 
            foreach (DataRow row in dt.Rows)
            {
                var productCategory = new ProductCategoryModel
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = Convert.ToString(row["Name"]),
                    SEO_Slug = Convert.ToString(row["SEO_Slug"]),
                    ImageUrl = Convert.ToString(row["ImageUrl"]),
                    SKU = Convert.ToString(row["SKU"]),
                    CategorySlug = Convert.ToString(row["CategorySlug"]),
                    CategoryName = Convert.ToString(row["CategoryName"])
                };

                productCategories.Add(productCategory);
            }
            return productCategories;

        }

        [HttpGet("product/{id}")]
        public IActionResult Product(string id)
        {
            List<ProductModel> products = GetProductData(id);
            return View("Product", products);
        }

        public List<ProductModel> GetProductData(string id)
        {
            string proc = "SP_GetProductDetailById";
            List<ProductModel> products = new List<ProductModel>();
            SqlParameter[] parameters = new SqlParameter[]
              {
                   new SqlParameter("@ProductId", SqlDbType.NVarChar) { Value = id }
              };

            DataTable dt = _sqlHelper.ExecuteProcdure(proc, parameters);
            foreach (DataRow row in dt.Rows)
            {
                var product = new ProductModel
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = Convert.ToString(row["Name"]),
                    SEO_Slug = Convert.ToString(row["SEO_Slug"]),
                    ImageUrl = Convert.ToString(row["ImageUrl"]),
                    SKU = Convert.ToString(row["SKU"]),
                };

                products.Add(product);
            }
            return products;

        }


        [HttpPost("contact")]
        public IActionResult Contact(ContactModel contact)
        {
            string query = @"INSERT INTO Contact 
                        (firstname, lastname, email, phone, ext, companyname, website, subject, [message]) 
                        VALUES 
                        (@FirstName, @LastName, @Email, @Phone, @Ext, @CompanyName, @Website, @Subject, @Message)";

            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@FirstName", contact.FirstName),
            new SqlParameter("@LastName", contact.LastName),
            new SqlParameter("@Email", contact.Email),
            new SqlParameter("@Phone", contact.Phone),
            new SqlParameter("@Ext", (object?)contact.Ext ?? DBNull.Value),
            new SqlParameter("@CompanyName", contact.CompanyName),
            new SqlParameter("@Website", contact.Website),
            new SqlParameter("@Subject", contact.Subject),
            new SqlParameter("@Message", contact.Message),
            new SqlParameter("@CreatedDate", DateTime.Now),
            };

            _sqlHelper.ExecuteNonQuery(query, parameters);

            return RedirectToAction("Contact");
        }
            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
