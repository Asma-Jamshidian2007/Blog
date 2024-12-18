
(function ($) {
  'use strict';


    $(document).on('ready', function(){

      var nav_menu = $('#nav_menu');
      nav_menu.slicknav({
        prependTo:'.mobile_menu',
      });

      var logo_carouael = $('.logo_carouael');
      var screen_carousel = $('.screen_carousel');
      var testimonials = $('.testimonials');

      testimonials.owlCarousel({
        loop:true,
        center:true,
        nav:false,
        dots:true,
        margin:30,
        autoplay:true,
        responsive:{
          0:{
             items:1
          },
          1000:{
            items:2
          },
          1200:{
            items:3
          }
        }

      })

      screen_carousel.owlCarousel({
        loop:true,
        items:2,
        center:true,
        nav:false,
        dots:true,
        margin:30,
        autoplay:true,

      })
      
      logo_carouael.owlCarousel({
        loop:true,
        nav:false,
        dots:false,
        margin:30,
        autoplay:true,
        responsive:{
          0:{
            items:1
          },
          400:{
            items:2
          },
          700:{
            items:4
          },
          1000:{
            items:5
          }
        }

      })

      $(".play_btn").modalVideo({channel:'youtube'});




      var apps_img          = document.getElementsByClassName('apps_img');
      var feature_img_right = document.getElementsByClassName('feature_img_right');
      var feature_img_left  = document.getElementsByClassName('feature_img_left');
      var apps_img          = document.getElementsByClassName('apps_img');

      new simpleParallax(apps_img, {
        overflow: true,
        orientation: 'right'
      });

      new simpleParallax(feature_img_right, {
        overflow: true,
        orientation: 'left'
      });
      new simpleParallax(feature_img_left, {
        overflow: true,
        orientation: 'right'
      });

      new simpleParallax(apps_img, {
        overflow: true,
        orientation: 'left'
      });

      $('.counter').counterUp({
            delay: 10,
            time: 1000
        });

        Splitting(); 

        ScrollOut({  
        threshold: .2,
        once: true
        });



      if ($('.scroll').length > 0) {

        var scrollers = $('.scroll a[href*="#"], a.upbtn');
        scrollers.on('click', function(e) {
          e.preventDefault()

          $('html, body').animate(
            {
              scrollTop: $($(this).attr('href')).offset().top
            },
            1000,
            'linear'
          )
        })

      }



    })

    new WOW().init();


   


    jQuery(window).on("load", function() {
        function preloadr() {
          var preloader = $('.mesh-loader');
          if(preloader.length){
          preloader.delay(200).fadeOut(500);
          }
      }
      preloadr(); 
    });


}(jQuery));



 



//End js main file......