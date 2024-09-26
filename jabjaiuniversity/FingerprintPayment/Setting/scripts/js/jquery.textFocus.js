$(document).ready(function(){
	//global vars
	var _txtFocus = jQuery(".text");
	var _txtFocusNum = jQuery(".textNumber");
	
	//Effects for both searchbox
	_txtFocus.focus(function(e){
		jQuery(this).addClass("active");
	});
	_txtFocus.blur(function(e){
		jQuery(this).removeClass("active");
	});
	
	_txtFocusNum.focus(function(e){
		jQuery(this).addClass("active");
	});
	_txtFocusNum.blur(function(e){
		jQuery(this).removeClass("active");
	});
	
});