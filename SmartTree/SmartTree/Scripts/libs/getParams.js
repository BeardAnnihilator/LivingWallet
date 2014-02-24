/*
 * Code retrieved here: http://www.codeproject.com/Articles/7924/Getting-URL-parameters-using-JavaScript
 * Modified by Alex on 11/19/2012 to make it more "library-like"
 */

function getUrlParameters() 
{
    var sURL = window.document.URL.toString();
    var parameters = new Array();
	
	if (sURL.indexOf("?") > 0)
	{
		var arrParams = sURL.split("?");
		var arrURLParams = arrParams[1].split("&");
		
		for (var i = 0; i < arrURLParams.length; i++)
		{
		    var sParam = arrURLParams[i].split("=");
		    var paramName = sParam[0];
		    var paramValue = (sParam[1] != "") ? unescape(sParam[1]) : "";
		    parameters[paramName] = paramValue;
		}
		
    }
    return parameters;
}

function getUrlParameterValue(paramName) {
    var parameters = getUrlParameters();
    var value = parameters[paramName];
    return value;
}