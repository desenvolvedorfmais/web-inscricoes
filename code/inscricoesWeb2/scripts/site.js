// JavaScript Document

var base_url = "http://www.congressoabert.com.br/_dev18/wp-content/themes/congressoabert/";

(function($) {
	var cache = [];

	$.preLoadImages = function() {
		
		var args_len = arguments.length;
		for (var i = args_len; i--;) {
			
			var cacheImage = document.createElement('img');
			cacheImage.src = arguments[i];
			cache.push(cacheImage);
			
		}
	}
})(jQuery)

jQuery.preLoadImages(base_url + "images/bg-torre.jpg" , base_url + "images/fotos/2.jpg", base_url +  "images/fotos/3.jpg", base_url +  "images/fotos/4.jpg", base_url +  "images/fotos/5.jpg", base_url +  "images/fotos/6.jpg", base_url +  "images/fotos/7.jpg", base_url +  "images/fotos/8.jpg", base_url +  "images/fotos/9.jpg", base_url +  "images/fotos/10.jpg", base_url +  "images/fotos/11.jpg", base_url +  "images/fotos/12.jpg", base_url +  "images/fotos/13.jpg", base_url +  "images/fotos/14.jpg", base_url +  "images/fotos/16.jpg", base_url +  "images/fotos/17.jpg", base_url + "images/fotos/1.jpg");
$(document).ready(function(){
	
	
	$(".img-container img").cjObjectScaler({method: "fill", fade: 500}, function() {});
	$("#imageSlide-container img").cjObjectScaler({method: "fill", fade: 500}, function() {});
	$(".img-container-banco img").cjObjectScaler({method: "fill", fade: 500}, function() {});
	$(".img-container-pal img").cjObjectScaler({method: "fill", fade: 500}, function() {});	
	$(".img-container-inscricao img").cjObjectScaler({method: "fill", fade: 500}, function() {});		
	
	$(".destaque-crop img").cjObjectScaler({method: "fill", fade: 500}, function() {});
	$(".destaque-crop iframe").cjObjectScaler({method: "fill", fade: 500}, function() {});					
	
	// jQuery(function($){
		// $("#tweet-content").tweet({
			// username: "congressoabert",
			// join_text: "auto",
			// avatar_size: 0,
			// count: 4,
			// loading_text: "Carregando Tweets..."
		// });
	// });

	
	
	var imgs = [
	
         base_url + 'images/fotos/2.jpg',
         base_url + 'images/fotos/3.jpg',
         base_url + 'images/fotos/4.jpg',
		 base_url + 'images/fotos/5.jpg',
		 base_url + 'images/fotos/6.jpg',
		 base_url + 'images/fotos/7.jpg',
		 base_url + 'images/fotos/8.jpg',
		 base_url + 'images/fotos/9.jpg',
		 base_url + 'images/fotos/10.jpg',
		 base_url + 'images/fotos/11.jpg',
		 base_url + 'images/fotos/12.jpg',
		 base_url + 'images/fotos/13.jpg',
		 base_url + 'images/fotos/14.jpg',
		 base_url + 'images/fotos/15.jpg',
		 base_url + 'images/fotos/16.jpg',
		 base_url + 'images/fotos/17.jpg',
		 base_url + 'images/fotos/1.jpg'];
    
	var cnt = imgs.length;

        $(function() {
            setInterval(Slider, 5000);
        });

        function Slider() {
        $('#imageSlide').animate({
			
			opacity: 0.0
		
		}, 800, 'swing', function() {
			
           $(this).attr('src', imgs[(imgs.length++) % cnt]).animate({
			
			opacity: 1.0
			
		
		}, 800, 'swing', function() {});
		   
        });
        }
	
	
	$("#bg-trans").fadeIn(500, function(){
		
		
	
			$("#destaques-container").animate({
			
				opacity: 1.0,
				top: '10px'
		
			}, 300, 'swing', function() {});		
		

		
	});
	
		
	
	$(".plus-but").mouseover(function(){
		
		
		
		 var idPai = $(this).parent().parent().attr("id");
		 
		 if (idPai) {
		 	var idBox = "#"+idPai.replace("container-", "");
		 	$(idBox).animate({
			
				
				opacity: 1.0
				
				
		
			}, 200, 'swing', function() {});
		

	
		 		
		 }	
	
		$(idBox).css("z-index","9999");
		
			
		
		
		
	});
	
	$(".plus-but").mouseleave(function(){
		
		
		 var idPai = $(this).parent().parent().attr("id");
		 
		 if (idPai) {
		 	var idBox = "#"+idPai.replace("container-", "");
		 	$(idBox).animate({
				
				
				opacity: 0.0
				
		
			}, 200, 'swing', function() {});
		

	
		 		
		 }	
	
		
			
		
		
		
	});
	
	
	$(".destaque-content").mouseover(function(){
		
		$(this).css("background","url(" + base_url + "images/bg-amarelo.png) repeat");
		$(this).animate({
			
			top: '140px'
		
		}, 200, 'swing', function() {});
		
		
		
		
		
		
	});
	
	$(".destaque-content").mouseleave(function(){
		
		$(this).css("background","url(" + base_url + "images/bg-azul.png) repeat");
		$(this).animate({
			
			top: '160px'
		
		}, 200, 'swing', function() {});
		
		
		
	});
	
	
	//menu
	
	$("ul.menu li").mouseover(function(){
		
		$(this).css("background","url(" + base_url + "images/menu/bg_over_menu.jpg) repeat");
		$(this).animate({
			
			opacity: 1.0 ,
			backgroundPositionY: '-150'
		
		}, 500, 'swing', function() {});
		
		
		
		
		
		
	});
	
	$("ul.menu li").mouseleave(function(){
		
		$(this).css("background","none");
		$(this).animate({
			
			 
		
		}, 100, 'swing', function() {});
		
		
		
	});
	
	
	
});
