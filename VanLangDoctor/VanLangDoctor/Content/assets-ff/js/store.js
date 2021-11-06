"use strict";
function swap1(image) {
    document.getElementById("images-select").src = image.href;
};
function slider_owl(slider_id, visible1, visible2, visible3, visible4, margin) {
    $(slider_id).owlCarousel({
        navigation: true,  
        slideSpeed: 500,
        singleItem: true,
        pagination: true,
        autoplay: false,
        margin: margin,
        autoplayTimeout: 2000,
        autoplayHoverPause: true,
        loop: true,
        responsiveClass: true,
        responsive: {
            0: {
                items: visible1
            },

            480: {
                items: visible2

            },

            767: {
                items: visible3
            },

            1025: {
                items: visible4
            }
        }
    });
}
$(document).ready(function() {
    // Slider products
    var windowns = $(window);
    var owl = $(".product-tab-content");
    var owl2 = $(".product-tab-content1");
    var owl3 = $("#secondary .sidebar-slider");
    var mason = $(".tab-product-all-mason .col-md-7 .slider-one-item");
    var menu_top_header3 = $(".header-v3 .menu-top");
    var menu_level1 = $("ul.menu-level-1");
    var mega_menu = $(".mega-menu");
    var sub_menu = $(".mega-menu .sub-menu");
    var header3_menu2 = $(".header-v3 .menu-top li.level-1 .menu-level2");
    var awe_page_loading = $('.awe-page-loading');
    var container_div = $(".container");
    awe_page_loading.delay(1000).fadeOut('400', function() {
        $(this).fadeOut()
    });
    var widthwindow = windowns.width();
    owl.css({
        "width": (widthwindow-parseInt(60, 10)) + "px"
    });
    owl2.css({
        "width": container_div.width() + "px"
    });
    owl3.css({
        "width": $("#secondary").width() + "px"
    });
    mason.css({
        "width": container_div.width() + "px"
    });
    if (widthwindow > parseInt(1024, 10)) {
        menu_top_header3.insertAfter($(".header-v3 .search.dropdown"));
        sub_menu.addClass("dropdown-menu");
        header3_menu2.addClass("dropdown-menu");
        menu_level1.addClass("dropdown-menu");
        mega_menu.insertAfter($(".header-top .logo"));
    } else {
        header3_menu2.removeClass("dropdown-menu");
        sub_menu.removeClass("dropdown-menu");
        menu_top_header3.insertAfter($(".header-v3 .header-top"));
        menu_level1.removeClass("dropdown-menu");
        mega_menu.insertAfter($("#header"));
    }
    windowns.on("orientationchange load resize", function() {
        var widthwindow = windowns.width();
        owl.css({
            "width": (widthwindow-parseInt(60, 10)) + "px"
        });
        owl2.css({
            "width": container_div.width() + "px"
        });
        owl3.css({
            "width": $("#secondary").width() + "px"
        });
        mason.css({
            "width": container_div.width() + "px"
        });
        if (widthwindow > parseInt(1024, 10)) {
            menu_top_header3.insertAfter($(".header-v3 .search.dropdown"));
            sub_menu.addClass("dropdown-menu");
            header3_menu2.addClass("dropdown-menu");
            menu_level1.addClass("dropdown-menu");
            mega_menu.insertAfter($(".header-top .logo"));
        } else {
            header3_menu2.removeClass("dropdown-menu");
            sub_menu.removeClass("dropdown-menu");
            menu_top_header3.insertAfter($(".header-v3 .header-top"));
            menu_level1.removeClass("dropdown-menu");
            mega_menu.insertAfter($("#header"));
        }
    });
    var menuLeft = $('.pushmenu-left');
    var menuHome6 = $('.menu-home6');
    var nav_click = $('.icon-pushmenu');
    nav_click.on("click", function(event) {
        event.stopPropagation();
        $(this).addClass('active');
        $('body').toggleClass('pushmenu-push-toleft');
        menuHome6.toggleClass('pushmenu-open');
    });
    $(".wrappage").on("click", function() {
        $(this).removeClass('active');
        $('body').removeClass('pushmenu-push-toright').removeClass('pushmenu-push-toleft');
        menuLeft.removeClass('pushmenu-open');
        menuHome6.removeClass('pushmenu-open');
        nav_click.removeClass('active');
    });
    $("#close-pushmenu").on("click", function() {
        $(this).removeClass('active');
        $('body').removeClass('pushmenu-push-toright');
        menuLeft.removeClass('pushmenu-open');
        nav_click.removeClass('active');
    });
    $("#close-pushmenu.close-left").on("click", function() {
        $('body').removeClass('pushmenu-push-toleft');
        menuHome6.removeClass('pushmenu-open');
        nav_click.removeClass('active');
    });

    if ($(".tp-banner").length) {
        $('.ver1 .tp-banner').revolution({
            delay: 9000,
            startwidth: 1860,
            startheight: 810,
            hideThumbs: 10,
            fullWidth: "on",
            forceFullWidth: "on"
        });
        $('.ver2 .tp-banner').revolution({
            delay: 9000,
            startwidth: 1860,
            startheight: 1030,
            hideThumbs: 10,
            fullWidth: "on",
            forceFullWidth: "on"
        });

        $('.ver3 .tp-banner').revolution({
            delay: 9000,
            startwidth: 870,
            startheight: 540,
            hideThumbs: 10,
            fullWidth: "on",
            forceFullWidth: "on"
        });
        $('.ver4 .tp-banner').revolution({
            delay: 9000,
            startwidth: 1170,
            startheight: 550,
            hideThumbs: 10,
            fullWidth: "on",
            forceFullWidth: "on"
        });
        $(".tp-leftarrow").html("");
        $(".tp-rightarrow").html("");
    }
    if ($(".product-img-box #image-view").length) {
        var widthwindow1 = windowns.width();
        if (widthwindow1 >= parseInt(1024, 10)) {
            $('#image').elevateZoom({
                zoomType: "inner",
                cursor: "crosshair",
                zoomWindowFadeIn: 375,
                zoomWindowFadeOut: 375
            });
        }
    }
    // Tabs
    // $(".tab-content").hide();
    $("ul.tabs").each(function() {
        $(this).children().first().addClass("active");
        $(this).next().children().first().show().addClass("active");
    });
    $("ul.tabs li").each(function() {
        $(this).on("click", function() {
            var tab_content = $(this).parent().next().children();
            $(this).parent().children().removeClass("active");
            $(this).addClass("active");
            tab_content.hide().removeClass("active");
            var activeTab = $(this).attr("rel");
            $("#" + activeTab).fadeIn(400).addClass("active");
        });
    });
    // End tabs
    // Slider
    slider_owl(".product-tab-content", 1, 2, 3, 5, 20);
    slider_owl(".furniture .product-tab-content1", 1, 2, 3, 4, 20);
    slider_owl(".slider-product-3-item .products", 1, 2, 2, 3);
    slider_owl(".slider-product-2-item .blog-post-inner", 1, 2, 2, 2);
    slider_owl(".blog-post-inner", 1, 2, 2, 3, 20);
    slider_owl(".brand-content", 2, 3, 4, 6, 0);

    slider_owl(".slider-one-item", 1, 1, 1, 1, 0);
    slider_owl(".slider-two-item", 1, 2, 2, 2, 30);
    slider_owl(".slider-three-item", 1, 2, 3, 3, 30);
    slider_owl(".upsell-product", 1, 2, 3, 5, 20);
    slider_owl(".slider-about",1,2,2,3,10);

    // click to zoom
    var img_box_thum = $(".product-img-box .thumb-content li");
    img_box_thum.first().addClass("active");
    img_box_thum.on("click", function() {
        img_box_thum.removeClass("active");
        $(this).addClass("active");
    });


    $('.owl-nav .owl-prev').html('<i class="fa fa-angle-left"></i>');
    $('.owl-nav .owl-next').html('<i class="fa fa-angle-right"></i>');
    // Click to Hover
    $('.dropdown').hover(function() {
        $(this).find('.dropdown-menu').stop(true, true).fadeIn(200).toggleClass("hover");
        $(this).toggleClass("active");
    }, function() {
        $(this).find('.dropdown-menu').stop(true, true).fadeOut(200).toggleClass("hover");
        $(this).toggleClass("active");
    });

    // Click Icon Menu Mobile
    $(".icon-menu-mobile").on("click", function() {
        $(".navbar-nav").slideToggle();
        $(this).toggleClass("active");
    });
    $(".header-v3 .icon-menu-mobile").on("click", function() {
        menu_top_header3.slideToggle();
    });
    $('li:has(ul)').addClass('hassub');
    $('li:has(".sub-menu")').addClass('images');
    $(".sub-menu img").parent().addClass("images");
    $(".mega-menu ul li a").after('<i class="fa fa-plus"></i>');
    $(".nav-home6 li a").after('<i class="fa fa-plus"></i>');

    var products = $(".products");
    var or_list = $(".ordering .list");
    var or_col = $(".ordering .col");
    or_list.on("click", function() {
        $(this).toggleClass("active");
        products.addClass("list-item");
        products.removeClass("ver2");
        or_col.removeClass("active");
    });
    or_col.on("click", function() {
        $(this).toggleClass("active");
        products.removeClass("list-item");
        products.addClass("ver2");
        or_list.removeClass("active");
    });

    $(".close-popup").on("click", function() {
        $(".popup-content").hide();
    });
    $(".closeqv").on("click", function() {
        $(".quickview-wrapper").hide();
    });
    $('#rtl').on("click", function() {
        $('body').toggleClass('rtl');
    });

    $("ul.product-categories li.hassub a").after('<i class="fa fa-caret-right"></i>');
    $("ul li.hassub i").on("click", function() {
        $(this).next().slideToggle();
        $(this).toggleClass("active");
        $(this).parent().toggleClass("active");
    });
    var megamenu_v2 = $(".megamenu-v2");
    $("#header .fa-bars").on("click", function() {
        megamenu_v2.addClass("show-ef");
    });

    $(".megamenu-v2 .fa-times").on("click", function() {
        megamenu_v2.removeClass("show-ef");
    });
    $(".form-check").on("click", function() {
       $(this).toggleClass("active"); 
    });

    var select_color = $(".select-color span");
    select_color.on("click", function() {
        select_color.removeClass('active');
        $(this).toggleClass('active');
    });
    /* event more-views click see big image. */
    var back_to_top = $('#back-to-top');
    if (back_to_top.length) {
        var scrollTrigger = parseInt(100, 10), // px
            backToTop = function() {
                var scrollTop = windowns.scrollTop();
                if (scrollTop > scrollTrigger) {
                    back_to_top.addClass('show');
                } else {
                    back_to_top.removeClass('show');
                }
            };
        windowns.on('scroll', function() {
            backToTop();
        });
        back_to_top.on('click', function(e) {
            e.preventDefault();
            $('html,body').animate({
                scrollTop: 0
            }, 700);
        });
    }

    if ($('.quantity').length) {
        var form_cart = $('form .quantity');
        form_cart.prepend('<span class="minus"><i class="fa fa-minus"></i></span>');
        form_cart.append('<span class="plus"><i class="fa fa-plus"></i></span>');

        var minus = form_cart.find($('.minus'));
        var plus = form_cart.find($('.plus'));

        minus.on('click', function() {
            var qty = $(this).parent().find('.qty');
            if (qty.val() <= 1) {
                qty.val(1);
            } else {
                qty.val((parseInt(qty.val(), 10) - 1));
            }

            var item_cart = $(this).parents('.item_cart');
            var item_number = item_cart.children('.product-quantity').find('input');
            var item_price = item_cart.children('.produc-price').children('input');
            var result =  item_cart.children('.total-price');
            result.html(function(){
                return '$'+(item_number.val() * item_price.val().replace('$', '')).toFixed(2);
            });

        });
        plus.on('click', function() {
            var qty = $(this).parent().find('.qty');
            qty.val((parseInt(qty.val(), 10) + 1));

            var item_cart = $(this).parents('.item_cart');
            var item_number = item_cart.children('.product-quantity').find('input');
            var item_price = item_cart.children('.produc-price').children('input');
            var result =  item_cart.children('.total-price');
            result.html(function(){
                return '$'+(item_number.val() * item_price.val().replace('$', '')).toFixed(2);
            });
        });
        $('.item_cart').each(function() {
        var answer = (parseInt($(this).children('.product-quantity').find('input').val(), 10) * $(this).children('.produc-price').find('input').val().replace('$', '')).toFixed(2);
        $(this).children('.total-price').html('$' + answer);
        });

        $('.item_cart').each(function() {
        $(this).children('.product-quantity').find('input').change(function() {
          var answer = (parseInt($(this).val(), 10) * $(this).parents('.item_cart').children('.produc-price').find('input').val().replace('$', '')).toFixed(2);
          $(this).parents('.item_cart').children('.total-price').html('$' + answer);
        });
        });
    }
    $(".calculate").on('click', function() {
        $(this).next().slideToggle();
        $(this).toggleClass("active");
    });
    $(".categories-home3 i.icon-click").on('click', function() {
        $(this).next().slideToggle();
        $(this).toggleClass("active");
    });
    var lightgallery = $('#lightgallery');
    if (lightgallery.length) {
        lightgallery.lightGallery();
    }
    if ($('.wow').length) {
        wow = new WOW({
            animateClass: 'animated',
            offset: 200,
            callback: function(box) {
                console.log("WOW: animating <" + box.tagName.toLowerCase() + ">")
            }
        });
        wow.init();
    }
    var slider_lookbook2 = $('.slider-loobook2');
    if (slider_lookbook2.length) {
        slider_lookbook2.slick({
            infinite: true,
            centerMode: true,
            slidesToShow: 1,
            slidesToScroll: 1
        });
    }
    var slider_slick_home8 = $(".slider-slick-home8")
    if (slider_slick_home8.length) {
        slider_slick_home8.slick({
            dots: true,
            infinite: true,
            centerMode: true,
            slidesToShow: 3,
            slidesToScroll: 3,
            responsive: [{
                breakpoint: 480,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
            }]
        });
    }
    if ($('.blog-masonry').length) {
        $('.grid').masonry({
            itemSelector: '.grid-item',
            columnWidth: '.grid-sizer',
            percentPosition: true
        });
    }
    var slide_for_dot_images = $('.slider-dot-images .slider-for');
    if (slide_for_dot_images.length) {
        slide_for_dot_images.slick({
            slidesToShow: 1,
            slidesToScroll: 1,
            arrows: false,
            infinite: true,
            asNavFor: '.slider-nav'
        });
        $('.slider-dot-images .slider-nav').slick({
            slidesToShow: 5,
            infinite: true,
            slidesToScroll: 1,
            asNavFor: '.slider-for',
            dots: false,
            focusOnSelect: true,
            centerMode: true,
            arrows: false
        });
    }
    var slide_for_pdt = $('.product-details-content .slider-for');
    if (slide_for_pdt.length) {
        slide_for_pdt.slick({
            slidesToShow: 1,
            slidesToScroll: 1,
            arrows: false,
            asNavFor: '.slider-nav'
        });
        $('.slider-nav').slick({
            slidesToShow: 4,
            slidesToScroll: 1,
            asNavFor: '.slider-for',
            dots: false,
            focusOnSelect: true
        });
        $('.zoom').zoom();
    }
});