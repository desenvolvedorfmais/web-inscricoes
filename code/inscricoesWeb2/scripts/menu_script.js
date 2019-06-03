
var ww = document.body.clientWidth;

jQuery(document).ready(function () {
    jQuery(".menubar li a").each(function () {
        if (jQuery(this).next().length > 0) {
            jQuery(this).addClass("parent");
        };
    })

    jQuery(".toggleMenu").click(function (e) {
        e.preventDefault();
        jQuery(this).toggleClass("active");
        jQuery(".menubar").toggle();
    });
    adjustMenu();
});

jQuery(window).bind('resize orientationchange', function () {
	ww = document.body.clientWidth;
	adjustMenu();
});

var adjustMenu = function() {
	if (ww < 768) {
	    jQuery(".toggleMenu").css("display", "inline-block");
	    if (!jQuery(".toggleMenu").hasClass("active")) {
	        jQuery(".menubar").hide();
		} else {
	        jQuery(".menubar").show();
		}
	    jQuery(".menubar li").unbind('mouseenter mouseleave');
	    jQuery(".menubar li a.parent").unbind('click').bind('click', function (e) {
			// must be attached to anchor element to prevent bubbling
			e.preventDefault();
			jQuery(this).parent("li").toggleClass("hover");
		});
	} 
	else if (ww >= 768) {
	    jQuery(".toggleMenu").css("display", "none");
	    jQuery(".menubar").show();
	    jQuery(".menubar li").removeClass("hover");
	    jQuery(".menubar li a").unbind('click');
	    jQuery(".menubar li").unbind('mouseenter mouseleave').bind('mouseenter mouseleave', function () {
		 	// must be attached to li so that mouseleave is not triggered when hover over submenu
	        jQuery(this).toggleClass('hover');
		});
	}
}

