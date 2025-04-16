using Microsoft.AspNetCore.Mvc;

namespace Consolidated_App.Models.FramworkModels
{
    public class NullJsonResult : JsonResult
    {

        public NullJsonResult() : base(null)
        {
        }
    }
}
