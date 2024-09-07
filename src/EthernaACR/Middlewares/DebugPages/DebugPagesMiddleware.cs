// Copyright 2021-present Etherna SA
// This file is part of Etherna ACR.
// 
// Etherna ACR is free software: you can redistribute it and/or modify it under the terms of the
// GNU Lesser General Public License as published by the Free Software Foundation,
// either version 3 of the License, or (at your option) any later version.
// 
// Etherna ACR is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
// without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License along with Etherna ACR.
// If not, see <https://www.gnu.org/licenses/>.

using Etherna.ACR.Middlewares.DebugPages.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Etherna.ACR.Middlewares.DebugPages
{
    public class DebugPagesMiddleware
    {
        private readonly RequestDelegate next;
        private readonly DebugPagesOptions options;

        public DebugPagesMiddleware(
            RequestDelegate next,
            IOptions<DebugPagesOptions> options)
        {
            ArgumentNullException.ThrowIfNull(options, nameof(options));

            this.next = next;
            this.options = options.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            ArgumentNullException.ThrowIfNull(context, nameof(context));

            if (context.Request.Path == options.ConfigurationPagePath)
            {
                var executor = context.RequestServices.GetRequiredService<IActionResultExecutor<ViewResult>>();
                var actionContext = new ActionContext(
                    context,
                    context.GetRouteData(),
                    new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor());
                var viewResult = new ViewResult()
                {
                    ViewData = new ViewDataDictionary(
                        new EmptyModelMetadataProvider(),
                        new ModelStateDictionary())
                    {
                        Model = new ConfigurationPageModel(
                            context.RequestServices.GetRequiredService<IConfiguration>(), options)
                    },
                    ViewName = "~/Middlewares/DebugPages/Views/ConfigurationPage.cshtml"
                };

                await executor.ExecuteAsync(actionContext, viewResult);
            }
            else if (context.Request.Path == options.RequestPagePath)
            {
                var executor = context.RequestServices.GetRequiredService<IActionResultExecutor<ViewResult>>();
                var actionContext = new ActionContext(
                    context,
                    context.GetRouteData(),
                    new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor());
                var viewResult = new ViewResult()
                {
                    ViewData = new ViewDataDictionary(
                        new EmptyModelMetadataProvider(),
                        new ModelStateDictionary())
                    {
                        Model = new RequestPageModel(context.Request, options)
                    },
                    ViewName = "~/Middlewares/DebugPages/Views/RequestPage.cshtml"
                };

                await executor.ExecuteAsync(actionContext, viewResult);
            }
            else
            {
                // Call the next delegate/middleware in the pipeline.
                await next(context);
            }
        }
    }
}
