using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using AFC.DAL;
using AFC.Models;
using AFC.Common;
using System.Data;
using Afc_web.Common;

namespace AFC.Controllers
{
    [Route("Configurator")]
    public class ConfiguratorController : Controller
    {
        private readonly ILogger<ConfiguratorController> _logger;
        private readonly SqlHelper _sqlHelper;

        public ConfiguratorController(ILogger<ConfiguratorController> logger,  SqlHelper sqlHelper)
        {
            _logger = logger;
            _sqlHelper = sqlHelper;
        }

        public List<Configurator> GetConfiguratorData(int ConfiguretypeId)
        {
            string query = "SELECT Id, ProductName,ProductType,ProductValue,ImageName,ImageUrl,Sku FROM Configurators where ConfiguratorTypeId=" + ConfiguretypeId;

            DataTable dt = _sqlHelper.ExecuteReader(query);
            List<Configurator> configurator_parts = new List<Configurator>();
            Configurator configurator_part;

            foreach (DataRow row in dt.Rows)
            {
                configurator_part = new Configurator();
                configurator_part.Id = Convert.ToInt32(row["Id"]);
                configurator_part.ProductName = Convert.ToString(row["productname"]);
                configurator_part.ProductType = Convert.ToString(row["producttype"]);
                configurator_part.ProductValue = Convert.ToInt32(row["productValue"]);
                configurator_part.ImageName = Convert.ToString(row["imagename"]);
                configurator_part.ImageUrl = Convert.ToString(row["imageurl"]);
                configurator_part.Sku = Convert.ToString(row["sku"]);
                configurator_parts.Add(configurator_part);
            }
            return configurator_parts;
        }


        [HttpGet("single-tier-configurator")]
        public IActionResult SingleTierConfigurator() 
        {
            List<Configurator> configurator_parts = GetConfiguratorData((int)Constant.ConfigureType.SingleTier);
            return View("SingleTierConfigurator", configurator_parts);
        }

        [HttpGet("duel-tier-configurator")]
        public IActionResult DuelTierConfigurator()
        {
            List<Configurator> configurator_parts = GetConfiguratorData((int)Constant.ConfigureType.DuelTier);
            return View("DuelTierConfigurator", configurator_parts);
        }

        [HttpGet("single-tier-corner-desk-configurator")]
        public IActionResult SingleTierConnerDeskConfigurator()
        {
            List<Configurator> configurator_parts = GetConfiguratorData((int)Constant.ConfigureType.SingleTierCorner);
            return View("SingleTierConnerDeskConfigurator", configurator_parts);
        }

        [HttpGet("bi-level-corner-desk-configurator")]
        public IActionResult BiLevelConnerDeskConfigurator()
        {
            List<Configurator> configurator_parts = GetConfiguratorData((int)Constant.ConfigureType.BiLevelCorner);
            return View("BiLevelCornerDeskConfigurator", configurator_parts);
        }

        
         [HttpGet("vertical-configurator")]
         public IActionResult VerticalConfigurator()
        {
            List<Configurator> configurator_parts = GetConfiguratorData((int)Constant.ConfigureType.VerticalConfigurator);
            return View("VerticalConfigurator", configurator_parts);
        }

        [HttpGet("horizontal-configurator")]
        public IActionResult HorizontalConfigurator()
        {
            List<Configurator> configurator_parts = GetConfiguratorData((int)Constant.ConfigureType.HorizontalConfigurator);
            return View("HorizontalConfigurator", configurator_parts);
        }


        [HttpGet("60-inches-pole-cart")]
        public IActionResult  _60InchesPolecart()
        {
            List<Configurator> configurator_parts = GetConfiguratorData((int)Constant.ConfigureType._60InchsPolecartConfigurator);
            return View("_60InchesPolecart", configurator_parts);
        }

        [HttpGet("72-inches-pole-cart")]
        public IActionResult _72InchesPolecart()
        {
            List<Configurator> configurator_parts = GetConfiguratorData((int)Constant.ConfigureType._72InchsPolecartConfigurator);
            return View("_72InchesPolecart", configurator_parts);
        }

        [HttpGet("computer-wallmounts")]
        public IActionResult ComputerWallmounts()
        {
            List<Configurator> configurator_parts = GetConfiguratorData((int)Constant.ConfigureType.ComputerWallmountsConfigurator);
            return View("ComputerWallmounts", configurator_parts);
        }

        [HttpGet("square-base-floor-post")]
        public IActionResult squareBaseFloorPost()
        {
            List<Configurator> configurator_parts = GetConfiguratorData((int)Constant.ConfigureType.SquareBaseFloorPost);
            return View("squareBaseFloorPost", configurator_parts);
        }
        [HttpGet("circular-base-floor-post")]
        public IActionResult CircularBaseFloorPost()
        {
            List<Configurator> configurator_parts = GetConfiguratorData((int)Constant.ConfigureType.CircularBaseFloorPost);
            return View("CircularBaseFloorPost", configurator_parts);
        }
        
        [HttpGet("smart-cart")]
        public IActionResult SmartCart()
        {
            List<Configurator> configurator_parts = GetConfiguratorData((int)Constant.ConfigureType.SmartCart);
            return View("SmartCart", configurator_parts);
        }
        [HttpGet("smart-cart-corner")]
        public IActionResult SmartCartCorner()
        {
            List<Configurator> configurator_parts = GetConfiguratorData((int)Constant.ConfigureType.SmartCartCorner);
            return View("SmartCartCorner", configurator_parts);
        }
        
        [HttpGet("smart-cart-l-type-desktop-surface")]
        public IActionResult SmartCartLTypeDesktopSurface()
        {
            List<Configurator> configurator_parts = GetConfiguratorData((int)Constant.ConfigureType.SmartCartLTypeDesktopSurface);
            return View("SmartCartLTypeDesktopSurface", configurator_parts);
        }

    }
}

