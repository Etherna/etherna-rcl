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

using System.Collections.Generic;

namespace Etherna.ACR.Pages.SharedModels
{
    public class SearchModel
    {
        public SearchModel(
            string? query,
            string? razorPage = default,
            string? razorPageHandler = default,
            Dictionary<string, string>? routeData = null,
            string searchParamName = "q")
        {
            Query = query ?? "";
            RazorPage = razorPage;
            RazorPageHandler = razorPageHandler;
            RouteData = routeData ?? new Dictionary<string, string>();
            SearchParamName = searchParamName;
        }

        public string Query { get; }
        public string? RazorPage { get; }
        public string? RazorPageHandler { get; }
        public IDictionary<string, string> RouteData { get; }
        public string SearchParamName { get; }
    }
}
