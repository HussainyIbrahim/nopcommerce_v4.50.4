﻿using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Error
{
    public record ErrorSearchModel : BaseSearchModel
    {
        public string Code { get; set; }
    }
}
