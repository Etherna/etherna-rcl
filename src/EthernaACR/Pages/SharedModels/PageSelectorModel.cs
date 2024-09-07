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

using System;
using System.Collections.Generic;

namespace Etherna.ACR.Pages.SharedModels
{
    public class PageSelectorModel
    {
        /// <summary>
        /// Constructor for PageSelector partial view Model
        /// </summary>
        /// <param name="currentPage">Current page</param>
        /// <param name="maxPage">Total page count</param>
        /// <param name="firstPage">First page number. Usually it can be 0 or 1</param>
        /// <param name="pageParamName">Name of page query parameter</param>
        /// <param name="routeData">Additional data to route</param>
        public PageSelectorModel(
            long currentPage,
            long maxPage,
            int firstPage = 0,
            string pageParamName = "p",
            Dictionary<string, string>? routeData = null)
        {
            if (currentPage < firstPage)
                throw new ArgumentOutOfRangeException(nameof(currentPage), $"Value can't be less than {firstPage}");
            if (maxPage < firstPage)
                throw new ArgumentOutOfRangeException(nameof(maxPage), $"Value can't be less than {firstPage}");

            CurrentPage = currentPage;
            MaxPage = maxPage;
            FirstPage = firstPage;
            PageParamName = pageParamName ?? throw new ArgumentNullException(nameof(pageParamName));
            RouteData = routeData ?? new Dictionary<string, string>();
        }

        public int FirstPage { get; }
        public long CurrentPage { get; }
        public long MaxPage { get; }
        public string PageParamName { get; }
        public IDictionary<string, string> RouteData { get; }
    }
}
