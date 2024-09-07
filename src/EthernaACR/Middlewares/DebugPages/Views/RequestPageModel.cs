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

using Microsoft.AspNetCore.Http;
using System;

namespace Etherna.ACR.Middlewares.DebugPages.Views
{
    public class RequestPageModel
    {
        // Constructor.
        public RequestPageModel(HttpRequest request, DebugPagesOptions options)
        {
            ArgumentNullException.ThrowIfNull(options, nameof(options));

            Request = request;
            Title = options.RequestPageTitle;
        }

        // Properties.
        public HttpRequest Request { get; }
        public string Title { get; }
    }
}
