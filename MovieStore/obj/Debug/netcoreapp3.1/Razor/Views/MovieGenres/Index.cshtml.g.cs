#pragma checksum "C:\Users\Dell\Source\Repos\afikmenashe21\MovieStore\MovieStore\Views\MovieGenres\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4d03157bdeb8fedb705d371383e0f9ca69d8fe6f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_MovieGenres_Index), @"mvc.1.0.view", @"/Views/MovieGenres/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Dell\Source\Repos\afikmenashe21\MovieStore\MovieStore\Views\_ViewImports.cshtml"
using MovieStore;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Dell\Source\Repos\afikmenashe21\MovieStore\MovieStore\Views\_ViewImports.cshtml"
using MovieStore.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4d03157bdeb8fedb705d371383e0f9ca69d8fe6f", @"/Views/MovieGenres/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4a8c2dc702b202c6daeaeb5df09bdab9162bf52c", @"/Views/_ViewImports.cshtml")]
    public class Views_MovieGenres_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<MovieStore.Models.MovieGenre>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/MovieDetails/detailsStyle.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Movies", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Dell\Source\Repos\afikmenashe21\MovieStore\MovieStore\Views\MovieGenres\Index.cshtml"
  
    ViewData [ "Title" ] = "Index";

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "4d03157bdeb8fedb705d371383e0f9ca69d8fe6f4859", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n<section class=\"slider\">\r\n    <div class=\"flexslider\">\r\n        <ul class=\"slides list-unstyled\">\r\n");
#nullable restore
#line 11 "C:\Users\Dell\Source\Repos\afikmenashe21\MovieStore\MovieStore\Views\MovieGenres\Index.cshtml"
             foreach ( var movie in Model )
                {


#line default
#line hidden
#nullable disable
            WriteLiteral("                <li>\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4d03157bdeb8fedb705d371383e0f9ca69d8fe6f6405", async() => {
                WriteLiteral("\r\n                        <div class=\"agile_tv_series_grid \">\r\n                            <div class=\"col-md-2 agile_tv_series_grid_left\">\r\n                                <div class=\"w3ls_market_video_grid1\">\r\n                                    <img");
                BeginWriteAttribute("src", " src=\"", 704, "\"", 729, 1);
#nullable restore
#line 19 "C:\Users\Dell\Source\Repos\afikmenashe21\MovieStore\MovieStore\Views\MovieGenres\Index.cshtml"
WriteAttributeValue("", 710, movie.Movie.Poster, 710, 19, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" alt=\" \" class=\"img-responsive\" />\r\n                                </div>\r\n                            </div>\r\n                            <div class=\"col-md-10 agile_tv_series_grid_right\">\r\n                                <p class=\"fexi_header\">");
#nullable restore
#line 23 "C:\Users\Dell\Source\Repos\afikmenashe21\MovieStore\MovieStore\Views\MovieGenres\Index.cshtml"
                                                  Write(movie.Movie.Name);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</p>
                                <div class=""row"">
                                    <div class=""col-sm-2"">
                                        <div class=""profile-details"">
                                            Story Line :
                                        </div>
                                    </div>
                                    <div class=""col-sm-10"">
                                        <div class=""profile-details-content"">
                                            ");
#nullable restore
#line 32 "C:\Users\Dell\Source\Repos\afikmenashe21\MovieStore\MovieStore\Views\MovieGenres\Index.cshtml"
                                       Write(movie.Movie.Storyline);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
                                        </div>
                                    </div>
                                </div>
                                <div class=""row"">
                                    <div class=""col-sm-2"">
                                        <div class=""profile-details"">
                                            Date of Release :
                                        </div>
                                    </div>
                                    <div class=""col-sm-10"">
                                        <div class=""profile-details-content"">
                                            ");
#nullable restore
#line 44 "C:\Users\Dell\Source\Repos\afikmenashe21\MovieStore\MovieStore\Views\MovieGenres\Index.cshtml"
                                       Write(movie.Movie.ReleaseDate.ToShortDateString());

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
                                        </div>
                                    </div>
                                </div>
                                <div class=""row"">
                                    <div class=""col-sm-2"">
                                        <div class=""profile-details"">
                                            Star Rating :
                                        </div>
                                    </div>
                                    <div class=""col-sm-10"">
                                        <div class=""profile-details-content"">
");
#nullable restore
#line 56 "C:\Users\Dell\Source\Repos\afikmenashe21\MovieStore\MovieStore\Views\MovieGenres\Index.cshtml"
                                              
                                                int intAverageRating = (int) Math.Floor( movie.Movie.AverageRating ); // 7.3 -> 7
                                                for ( int i = 0 ; i < intAverageRating ; i++ ) // 7
                                                    {

#line default
#line hidden
#nullable disable
                WriteLiteral("                                                    <a href=\"#\"><i class=\"fa fa-star\" aria-hidden=\"true\"></i></a>\r\n");
#nullable restore
#line 61 "C:\Users\Dell\Source\Repos\afikmenashe21\MovieStore\MovieStore\Views\MovieGenres\Index.cshtml"
                                                    }
                                                if ( movie.Movie.AverageRating - intAverageRating > 0 ) // 7.5 - 7 = 0.5
                                                    {

#line default
#line hidden
#nullable disable
                WriteLiteral("                                                    <a href=\"#\"><i class=\"fa fa-star-half-o\" aria-hidden=\"true\"></i></a>\r\n");
#nullable restore
#line 65 "C:\Users\Dell\Source\Repos\afikmenashe21\MovieStore\MovieStore\Views\MovieGenres\Index.cshtml"
                                                    }
                                                else
                                                    {

#line default
#line hidden
#nullable disable
                WriteLiteral("                                                    <a href=\"#\"><i class=\"fa fa-star-o\" aria-hidden=\"true\"></i></a>\r\n");
#nullable restore
#line 69 "C:\Users\Dell\Source\Repos\afikmenashe21\MovieStore\MovieStore\Views\MovieGenres\Index.cshtml"
                                                    }
                                                for ( int i = 1 ; i < 10 - intAverageRating ; i++ ) //10-7 = 3
                                                    {

#line default
#line hidden
#nullable disable
                WriteLiteral("                                                    <a href=\"#\"><i class=\"fa fa-star-o\" aria-hidden=\"true\"></i></a>\r\n");
#nullable restore
#line 73 "C:\Users\Dell\Source\Repos\afikmenashe21\MovieStore\MovieStore\Views\MovieGenres\Index.cshtml"
                                                    }
                                            

#line default
#line hidden
#nullable disable
                WriteLiteral("                                        </div>\r\n                                    </div>\r\n                                </div>\r\n\r\n\r\n                            </div>\r\n                        </div>\r\n                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 15 "C:\Users\Dell\Source\Repos\afikmenashe21\MovieStore\MovieStore\Views\MovieGenres\Index.cshtml"
                                                                      WriteLiteral(movie.Movie.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                </li>\r\n");
#nullable restore
#line 84 "C:\Users\Dell\Source\Repos\afikmenashe21\MovieStore\MovieStore\Views\MovieGenres\Index.cshtml"

                }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </ul>\r\n    </div>\r\n</section>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<MovieStore.Models.MovieGenre>> Html { get; private set; }
    }
}
#pragma warning restore 1591
