//------------------------------------------------------------
// On document ready
//------------------------------------------------------------

$(function () {
    if(
    	BrowserDetect.browser =='Explorer' && parseInt(BrowserDetect.version.split('.')[0]) < 9
    	&& $.cookie('hasSeenBrowserWarning')!='true'

    ){
	    var browserWarningHtml = 	

		'<div id="browserWarningModal" class="modal fade hide">'+
	        '<div class="modal-header">'+
	            '<a class="close" data-dismiss="modal">×</a>'+
	            '<h3>Compatibilité navigateur</h3>'+
	        '</div>'+
	        '<div class="modal-body clearfix">'+
			    "<p>Vous utilisez <b>"+BrowserDetect.browser+" "+(BrowserDetect.version.split('.')[0])+"</b>. </p>"+
			    "<p>"+
			    	"Le site de Big Moustache est compatible avec Internet Exporer à partir de la version 9. Il est donc possible que vous rencontriez des problèmes d'affichage. "+
			    	"Pour un surf décontracté de la moustache, utilisez Chrome ou Firefox !"+
			    "</p>"+
	        '</div>'+
	        '<div class="modal-footer">'+
	            '<a href="#" data-dismiss="modal" class="btn btn-primary">Ok</a>'+
	        '</div>'+
	    '</div>';

		$('body').append(browserWarningHtml);
		$('#browserWarningModal').modal();

		$.cookie('hasSeenBrowserWarning', 'true', { expires: 1, path: '/' });
    }
})