#pragma checksum "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\Movies\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "74594fcd0124bce8187926c7ee7877c98fd66792"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Movies_Details), @"mvc.1.0.view", @"/Views/Movies/Details.cshtml")]
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
#line 1 "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\_ViewImports.cshtml"
using MovieStore;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\_ViewImports.cshtml"
using MovieStore.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\Movies\Details.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"74594fcd0124bce8187926c7ee7877c98fd66792", @"/Views/Movies/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4a8c2dc702b202c6daeaeb5df09bdab9162bf52c", @"/Views/_ViewImports.cshtml")]
    public class Views_Movies_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<MovieStore.Models.Movie>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/reviewSearch.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/MovieDetails/detailsStyle.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Reviews", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/user.jpg"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", new global::Microsoft.AspNetCore.Html.HtmlString("One movies"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString(" "), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/reviewSearch.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/jquery.dotdotdot.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n");
#nullable restore
#line 5 "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\Movies\Details.cshtml"
  
    ViewData["Title"] = "Details";
    Layout = "_Layout2";


#line default
#line hidden
#nullable disable
            WriteLiteral("<!--\r\nauthor: W3layouts\r\nauthor URL: http://w3layouts.com\r\nLicense: Creative Commons Attribution 3.0 Unported\r\nLicense URL: http://creativecommons.org/licenses/by/3.0/\r\n-->\r\n<!-- single -->\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "74594fcd0124bce8187926c7ee7877c98fd667927540", async() => {
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
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "74594fcd0124bce8187926c7ee7877c98fd667928655", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"

<div class=""single-page-agile-main"">
    <div class=""container"">
        <div class=""single-page-agile-info"">
            <!-- /movie-browse-agile -->
            <div class=""container"">
                <div class=""row"">
                    <div class=""col-md-4 video-grid-single-page-agileits"">
                        <img");
            BeginWriteAttribute("src", " src=", 788, "", 806, 1);
#nullable restore
#line 27 "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\Movies\Details.cshtml"
WriteAttributeValue("", 793, Model.Poster, 793, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("alt", " alt=\"", 806, "\"", 812, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"img-responsive\" id=\"single-poster\" />\r\n                    </div>\r\n                    <div class=\"col-md-8 coloum\">\r\n                        <p class=\"fexi_header\">");
#nullable restore
#line 30 "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\Movies\Details.cshtml"
                                          Write(Model.Name);

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
#line 39 "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\Movies\Details.cshtml"
                               Write(Model.Storyline);

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
#line 51 "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\Movies\Details.cshtml"
                               Write(Model.ReleaseDate.ToShortDateString());

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
                                    Genres :
                                </div>
                            </div>
                            <div class=""col-sm-10"">
                                <div class=""profile-details-content"">
");
#nullable restore
#line 63 "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\Movies\Details.cshtml"
                                       if (ViewBag.genres != null)
                                        {
                                            for (int i = 0; i < ViewBag.genres.Count - 1; i++)
                                            {
                                                var genre = ViewBag.genres[i].Type.ToString();

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                <a href=\"genres.html\" style=\"color:black\">");
#nullable restore
#line 68 "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\Movies\Details.cshtml"
                                                                                     Write(genre);

#line default
#line hidden
#nullable disable
            WriteLiteral(" | </a>\r\n");
#nullable restore
#line 69 "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\Movies\Details.cshtml"
                                            }
                                            var genreLast = ViewBag.genres[ViewBag.genres.Count - 1].Type.ToString();

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <a href=\"genres.html\" style=\"color:black\">");
#nullable restore
#line 71 "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\Movies\Details.cshtml"
                                                                                 Write(genreLast);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n");
#nullable restore
#line 72 "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\Movies\Details.cshtml"
                                        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                </div>
                            </div>
                        </div>
                        <div class=""row"">
                            <div class=""col-sm-2"">
                                <div class=""profile-details"">
                                    Actors :
                                </div>
                            </div>
                            <div class=""col-sm-10"">
                                <div class=""profile-details-content"">
");
#nullable restore
#line 84 "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\Movies\Details.cshtml"
                                       if (ViewBag.actors != null)
                                        {
                                            for (int i = 0; i < ViewBag.actors.Count - 1; i++)
                                            {
                                                var actor = ViewBag.actors[i].Name;

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                <a href=\"genres.html\" style=\"color:black\">");
#nullable restore
#line 89 "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\Movies\Details.cshtml"
                                                                                     Write(actor);

#line default
#line hidden
#nullable disable
            WriteLiteral(" | </a>\r\n");
#nullable restore
#line 90 "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\Movies\Details.cshtml"
                                            }
                                            var actorLast = ViewBag.actors[ViewBag.actors.Count - 1].Name;

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <a href=\"genres.html\" style=\"color:black\">");
#nullable restore
#line 92 "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\Movies\Details.cshtml"
                                                                                 Write(actorLast);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n");
#nullable restore
#line 93 "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\Movies\Details.cshtml"
                                        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                </div>
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
#line 105 "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\Movies\Details.cshtml"
                                      

                                        int intAverageRating = (int)Math.Floor(Model.AverageRating); // 7.3 -> 7
                                        for (int i = 0; i < intAverageRating; i++) // 7
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <span href=\"#\"><i class=\"fa fa-star\" aria-hidden=\"true\"></i></span>\r\n");
#nullable restore
#line 111 "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\Movies\Details.cshtml"
                                        }
                                        if (Model.AverageRating - intAverageRating > 0) // 7.5 - 7 = 0.5
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <span href=\"#\"><i class=\"fa fa-star-half-o\" aria-hidden=\"true\"></i></span>\r\n");
#nullable restore
#line 115 "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\Movies\Details.cshtml"
                                        }
                                        else
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <span href=\"#\"><i class=\"fa fa-star-o\" aria-hidden=\"true\"></i></span>\r\n");
#nullable restore
#line 119 "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\Movies\Details.cshtml"
                                        }
                                        for (int i = 1; i < 10 - intAverageRating; i++) //10-7 = 3
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <span href=\"#\"><i class=\"fa fa-star-o\" aria-hidden=\"true\"></i></span>\r\n");
#nullable restore
#line 123 "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\Movies\Details.cshtml"
                                        }
                                    

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                </div>
                            </div>
                        </div>
                        <div style=""margin-right:-15px ; margin-left:75px ; margin-top:20px"">
                            <iframe width=""560"" height=""315""");
            BeginWriteAttribute("src", " src=\"", 6721, "\"", 6741, 1);
#nullable restore
#line 129 "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\Movies\Details.cshtml"
WriteAttributeValue("", 6727, Model.Trailer, 6727, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" frameborder=""0"" allow=""accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"" allowfullscreen></iframe>
                        </div>
                    </div>
                </div>
                <div class=""song-grid-right"">
                    <div class=""share"">
                        <h5>Share this</h5>
                        <div class=""single-agile-shar-buttons"">
                            <ul>
                                <li>
                                    <div class=""fb-like"" data-href=""https://www.facebook.com/w3layouts"" data-layout=""button_count"" data-action=""like"" data-size=""small"" data-show-faces=""false"" data-share=""false""></div>
                                    <script>
                                        (function (d, s, id) {
                                            var js, fjs = d.getElementsByTagName(s)[0];
                                            if (d.getElementById(id)) return;
                                ");
            WriteLiteral(@"            js = d.createElement(s); js.id = id;
                                            js.src = ""//connect.facebook.net/en_GB/sdk.js#xfbml=1&version=v2.7"";
                                            fjs.parentNode.insertBefore(js, fjs);
                                        }(document, 'script', 'facebook-jssdk'));</script>
                                </li>
                                <li>
                                    <div class=""fb-share-button"" data-href=""https://www.facebook.com/w3layouts"" data-layout=""button_count"" data-size=""small"" data-mobile-iframe=""true""><a class=""fb-xfbml-parse-ignore"" target=""_blank"" href=""https://www.facebook.com/sharer/sharer.php?u=https%3A%2F%2Fwww.facebook.com%2Fw3layouts&amp;src=sdkpreparse"">Share</a></div>
                                </li>
                                <li class=""news-twitter"">
                                    <a href=""https://twitter.com/w3layouts"" class=""twitter-follow-button"" data-show-count=""false"">Follow w3layouts");
            WriteLiteral(@"</a>
                                    <script async src=""//platform.twitter.com/widgets.js"" charset=""utf-8""></script>
                                </li>
                                <li>
                                    <a href=""https://twitter.com/intent/tweet?screen_name=w3layouts"" class=""twitter-mention-button"" data-show-count=""false"">Tweet to w3layouts</a>
                                    <script async src=""//platform.twitter.com/widgets.js"" charset=""utf-8""></script>
                                </li>
                                <li>
                                    <!-- Place this tag where you want the +1 button to render. -->
                                    <div class=""g-plusone"" data-size=""medium""></div>

                                    <!-- Place this tag after the last +1 button tag. -->
                                    <script type=""text/javascript"">
                                        (function () {
                                            v");
            WriteLiteral(@"ar po = document.createElement('script'); po.type = 'text/javascript'; po.async = true;
                                            po.src = 'https://apis.google.com/js/platform.js';
                                            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(po, s);
                                        })();
                                    </script>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class=""clearfix""> </div>

                <div class=""s01"">
                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "74594fcd0124bce8187926c7ee7877c98fd6679224809", async() => {
                WriteLiteral("\r\n                        <fieldset>\r\n");
                WriteLiteral(@"                            <h1 style=""color:black "">Search Review:</h1>
                        </fieldset>
                        <div class=""inner-form"">

                            <div class=""input-field second-wrap"">
                                <input id=""fromYears"" type=""text"" placeholder=""From"" />
                            </div>

                            <div class=""input-field second-wrap"">
                                <input id=""toYears"" type=""text"" placeholder=""To"" />
                            </div>

                            <div class=""input-field second-wrap"">
                                <input id=""user"" type=""text"" placeholder=""User"" />
                            </div>

                            <div class=""input-field second-wrap"">
                                <input id=""content"" type=""text"" placeholder=""Content"" />
                            </div>

                        </div>
                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                </div>\r\n\r\n                <div class=\"clearfix\"> </div>\r\n\r\n                <div class=\"all-comments\">\r\n");
#nullable restore
#line 210 "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\Movies\Details.cshtml"
                     if (Context.Session.GetString("Type") != null)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <div class=\"all-comments-info\">\r\n                            <a href=\"#\">Comments</a>\r\n                            <div class=\"agile-info-wthree-box\">\r\n                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "74594fcd0124bce8187926c7ee7877c98fd6679227770", async() => {
                WriteLiteral(@"
                                    <input type=""text"" placeholder=""Headline"" name=""Headline"">
                                    <input type=""text"" placeholder=""Content"" name=""Content"">
                                    <input type=""text"" placeholder=""Rating"" name=""Rating"">
                                    <input name=""movieid""");
                BeginWriteAttribute("value", " value=\"", 12398, "\"", 12415, 1);
#nullable restore
#line 219 "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\Movies\Details.cshtml"
WriteAttributeValue("", 12406, Model.Id, 12406, 9, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" type=\"hidden\" />\r\n                                    <input name=\"userid\"");
                BeginWriteAttribute("value", " value=\"", 12491, "\"", 12537, 1);
#nullable restore
#line 220 "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\Movies\Details.cshtml"
WriteAttributeValue("", 12499, Context.Session.GetString( "UserId" ), 12499, 38, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" type=\"hidden\" />\r\n                                    <input type=\"submit\" value=\"SEND\">\r\n                                    <div class=\"clearfix\"> </div>\r\n                                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            </div>\r\n                        </div>\r\n");
#nullable restore
#line 226 "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\Movies\Details.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    <div class=\"media-grids\">\r\n");
#nullable restore
#line 230 "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\Movies\Details.cshtml"
                           if (ViewBag.reviews != null)
                                foreach (Review review in ViewBag.reviews)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <div class=\"media\">\r\n                                        <h5>");
#nullable restore
#line 234 "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\Movies\Details.cshtml"
                                       Write(review.Headline);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                                        <div class=\"media-left\">\r\n                                            <a href=\"#\">\r\n                                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "74594fcd0124bce8187926c7ee7877c98fd6679232080", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                            </a>\r\n                                        </div>\r\n                                        <div class=\"media-body\">\r\n                                            <p>");
#nullable restore
#line 241 "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\Movies\Details.cshtml"
                                          Write(review.Content);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                                            <div>Written by: ");
#nullable restore
#line 242 "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\Movies\Details.cshtml"
                                                        Write(review.Author.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                                            <div>Published: ");
#nullable restore
#line 243 "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\Movies\Details.cshtml"
                                                       Write(review.Published.ToShortDateString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                                            <div>Rating: ");
#nullable restore
#line 244 "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\Movies\Details.cshtml"
                                                    Write(review.Rating);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                                        </div>\r\n                                    </div>\r\n");
#nullable restore
#line 247 "C:\Users\amttr\source\repos\MovieStore\MovieStore\Views\Movies\Details.cshtml"
                                } 

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </div>\r\n                </div>\r\n            </div>\r\n\r\n\r\n\r\n            <div class=\"clearfix\"> </div>\r\n        </div>\r\n\r\n    </div>\r\n    <!-- //w3l-latest-movies-grids -->\r\n</div>\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "74594fcd0124bce8187926c7ee7877c98fd6679235358", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "74594fcd0124bce8187926c7ee7877c98fd6679236398", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<MovieStore.Models.Movie> Html { get; private set; }
    }
}
#pragma warning restore 1591
