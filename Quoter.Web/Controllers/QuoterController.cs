﻿using System;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using QuoterWeb;
using RoslynQuoter;

namespace QuoterService.Controllers
{
    [Route("api/[controller]")]
    public class QuoterController : Controller
    {
        [HttpPost]
        public IActionResult Post([FromBody] QuoterRequestArgument arguments)
        {
            string prefix = null;

            string responseText = "Quoter is currently down for maintenance. Please check back later.";
            if (arguments is null)
            {
                return BadRequest(responseText);
            }

            if (string.IsNullOrEmpty(arguments.SourceText))
            {
                responseText = "Please specify the source text.";
            }
            else
            {
                try
                {
                    var quoter = new Quoter
                    {
                        OpenParenthesisOnNewLine = arguments.OpenCurlyOnNewLine,
                        ClosingParenthesisOnNewLine = arguments.CloseCurlyOnNewLine,
                        UseDefaultFormatting = !arguments.PreserveOriginalWhitespace,
                        RemoveRedundantModifyingCalls = !arguments.KeepRedundantApiCalls,
                        ShortenCodeWithUsingStatic = !arguments.AvoidUsingStatic
                    };

                    responseText = quoter.QuoteText(arguments.SourceText, arguments.NodeKind);

                    if (arguments.ReadyToRun)
                    {
                        responseText = ReadyToRunHelper.CreateReadyToRunCode(arguments, responseText);
                    }
                }
                catch (Exception ex)
                {
                    responseText = ex.ToString();

                    prefix = "Congratulations! You've found a bug in Quoter! Please open an issue at <a href=\"https://github.com/KirillOsenkov/RoslynQuoter/issues/new\" target=\"_blank\">https://github.com/KirillOsenkov/RoslynQuoter/issues/new</a> and paste the code you've typed above and this stack:";
                }
            }
            
            responseText = HttpUtility.HtmlEncode(responseText);

            if (prefix != null)
            {
                responseText = "<div class=\"error\"><p>" + prefix + "</p><p>" + responseText + "</p><p><br/>P.S. Sorry!</p></div>";
            }

            return Ok(responseText);
        }
    }
}