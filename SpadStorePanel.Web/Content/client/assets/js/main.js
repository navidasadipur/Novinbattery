(function ($) {
    "use strict";
    jQuery(document).ready(function ($) {

        /*----------------------
            Side bar
        -----------------------*/
        var sideSidebarArea = $('#slide-sidebar-area');
        var bodyOvrelay = $('#body-overlay');
        var cartSidebarArea = $('#cart-sidebar-area');
        var searchPopup = $('#search-popup');

        $(document).on('click', '#side-menu', function (e) {
            e.preventDefault();
            sideSidebarArea.addClass('active');
            bodyOvrelay.addClass('active');
        });
        $(document).on('click', '.side-sidebar-close-btn', function (e) {
            e.preventDefault();
            sideSidebarArea.removeClass('active');
            bodyOvrelay.removeClass('active');
            cartSidebarArea.removeClass('active');
        });
        $(document).on('click', '#cart', function (e) {
            e.preventDefault();
            cartSidebarArea.addClass('active');
            bodyOvrelay.addClass('active');
        });
        $(document).on('click', '#body-overlay', function (e) {
            e.preventDefault();
            sideSidebarArea.removeClass('active');
            bodyOvrelay.removeClass('active');
            searchPopup.removeClass('active');
            cartSidebarArea.removeClass('active');
        });
        $(document).on('click', '#search', function (e) {
            e.preventDefault();
            searchPopup.addClass('active');
            bodyOvrelay.addClass('active');
        });
        $(document).on('click', '.remove-cart', function (e) {
            e.preventDefault();
            $(this).parent().parent().hide(1000);
        });

        /*--------------------
            wow js init
        ---------------------*/
        new WOW().init();
        /*------------------------
            bootstrap select
        -------------------------*/
        $('.selectpicker').select2();

        /*-------------------------
            magnific popup activation
        -------------------------*/
        $('.video-play-btn,.video-popup,.small-vide-play-btn').magnificPopup({
            type: 'video'
        });
        /*------------------
            back to top
        ------------------*/
        $(document).on('click', '.back-to-top', function () {
            $("html,body").animate({
                scrollTop: 0
            }, 2000);
        });
        /*-------------------------
            counter section activation
        ---------------------------*/
        var counternumber = $('.count');
        counternumber.counterUp({
            delay: 20,
            time: 5000
        });
        /*-------------------------------
            sellers products close btn
        --------------------------------*/
        $(document).on('click', '.sp-close-btn', function (e) {
            var el = $(this);
            e.preventDefault();
            el.parent().parent().parent().hide(1000);

        });
        /*-----------------------------
            Cart Page Quantity 
        -----------------------------*/
        $(document).on('click', '.qtminus', function () {
            var el = $(this);
            var total = el.parent().parent().find('.qttotal').text();
            if (total >= 1) {
                total--;
            }
            el.parent().parent().find('.qttotal').text(total);
        });
        $(document).on('click', '.qtplus', function () {
            var el = $(this);
            var total = el.parent().parent().find('.qttotal').text();
            total++;

            el.parent().parent().find('.qttotal').text(total);
        });
        /*-------------------------------
            Remove Item From Cart table
        -------------------------------*/
        $(document).on('click', '.cart-remove-item', function () {
            var el = $(this);
            el.parent().parent().parent().hide(1000);
        });

        /*---------------------------
            Recently Added carousel
        ---------------------------*/
        var recentlyaddedResponsive = {
            0: {
                items: 1
            },
            768: {
                items: 2
            },
            960: {
                items: 3
            },
            1200: {
                items: 4
            },
            1920: {
                items: 4
            }
        };
        owlCarsouelActivate('#recently-added-carousel', true, recentlyaddedResponsive);

        /*---------------------------
            Product Details carousel
        ---------------------------*/
        var productResponsive = {
            0: {
                items: 1
            },
            768: {
                items: 1
            },
            960: {
                items: 1
            },
            1200: {
                items: 1
            },
            1920: {
                items: 1
            }
        };

        owlCarsouelActivate('#product-details-slider', false, productResponsive, true, true);

        /*---------------------------
            Team Member carousel
        ---------------------------*/
        var teamResponsive = {
            0: {
                items: 1
            },
            768: {
                items: 2
            },
            995: {
                items: 2
            },
            1200: {
                items: 2
            },
            1920: {
                items: 3
            }
        };
        owlCarsouelActivate('#team-carousel', false, teamResponsive);


        /*---------------------------
            testimonial carousel
        ---------------------------*/
        var testimonialRespnosive = {
            0: {
                items: 1
            },
            768: {
                items: 2
            },
            960: {
                items: 2
            },
            1200: {
                items: 2
            },
            1920: {
                items: 3
            }
        };
        owlCarsouelActivate('#testimonial-carousel', false, testimonialRespnosive);

        /*---------------------------
            brand carousel one
        ---------------------------*/
        var brandResponsive = {
            0: {
                items: 1
            },
            414: {
                items: 1,
            },
            768: {
                items: 3
            },
            960: {
                items: 4
            },
            1200: {
                items: 5
            },
            1920: {
                items: 5
            }
        }
        owlCarsouelActivate('#brand-logo-carousel-one', false, brandResponsive);


        /*---------------------------
            Dropipy 
         ---------------------------*/
        var $dropify = $('.dropify');
        if ($dropify.length > 0) {
            $dropify.dropify();
        }
        /*---------------------------
            Data table 
         ---------------------------*/
        var $dataTable = $('#datatableOne');
        if ($dataTable.length > 0) {
            $('#datatableOne').DataTable({
                paging: false,
                searching: false
            });
        }
        /*---------------------------------
            Projects Masonry activation
        ----------------------------------*/
        var projectsContainer = $('#best-seller-home-6-masonry');
        if (projectsContainer.length > 0) {
            projectsContainer.imagesLoaded(function () {
                var projectsMasonry = $('#best-seller-home-6-masonry').isotope({
                    masonry: {
                        columnWidth: 0,
                        gutter: 0
                    }
                });
                $(document).on('click', '.best-seller-home-6-filter-menu li', function () {
                    var filterValue = $(this).attr('data-filter');
                    projectsMasonry.isotope({
                        filter: filterValue
                    });
                });
            });
        }
        /*---------------------------------
            Projects Active class
        ----------------------------------*/
        var projectsMenu = '.best-seller-home-6-filter-menu li';
        $(document).on('click', projectsMenu, function () {
            $(this).siblings().removeClass('active');
            $(this).addClass('active');
        });

        /*------------------------
             Add To Cart Animation
        ------------------------*/
        $(document).on('click', '.addtocart', function (e) {
            e.preventDefault();
            var cart = $('#cart');
            var imgtodrag = $(this).parent().parent().find("img").eq(0);
            // console.log(imgtodrag);
            if (imgtodrag) {
                var imgclone = imgtodrag.clone()
                    .offset({
                        top: imgtodrag.offset().top,
                        left: imgtodrag.offset().left
                    })
                    .css({
                        'opacity': '0.5',
                        'position': 'absolute',
                        'height': '150px',
                        'width': '150px',
                        'z-index': '100'
                    })
                    .appendTo($('body'))
                    .animate({
                        'top': cart.offset().top + 10,
                        'left': cart.offset().left + 10,
                        'width': 75,
                        'height': 75
                    }, 1000);

                setTimeout(function () {
                    cart.removeClass("shake");
                    cart.addClass("shake");
                }, 1000);
                setTimeout(function () {
                    cart.removeClass("shake");
                }, 1800);

                imgclone.animate({
                    'width': 0,
                    'height': 0
                }, function () {
                    $(this).detach()
                });
            }
        });


        //owl carousel activate function 

        function owlCarsouelActivate(selector, nav, responsive, thumbs = false, thumbsPrerendered = false, loop = true, autoplay = true, margin = 30, dot = false) {
            var $selector = $(selector);
            if ($selector.length > 0) {
                $selector.owlCarousel({
                    loop: loop,
                    autoplay: autoplay, //true if you want enable autoplay
                    autoPlayTimeout: 1000,
                    margin: margin,
                    dots: dot,
                    nav: nav,
                    thumbs: thumbs,
                    navText: ['<i class="fas fa-angle-left"></i>', '<i class="fas fa-angle-right"></i>'],
                    thumbsPrerendered: thumbsPrerendered,
                    responsive: responsive
                });
            }
        }
        /**-------------------------------
         * Contact Js
         * -----------------------------**/
        $(document).on('submit', '#get_in_touch', function (e) {
            e.preventDefault();
            //element id
            var formID = $('#get_in_touch');
            var first_nameID = $('#first_name');
            var last_nameID = $('#last_name');
            var subjectID = $('#subject');
            var emailID = $('#email');
            var messageID = $('#message');
            //element value
            var first_name = $('#first_name').val();
            var last_name = $('#last_name').val();
            var subject = $('#subject').val();
            var email = $('#email').val();
            var message = $('#message').val();

            if (!first_name) {
                first_nameID.removeClass('error');
                first_nameID.addClass('error').attr('placeholder', 'Please Enter First Name');
            } else {
                first_nameID.removeClass('error');
            }

            if (!last_name) {
                last_nameID.removeClass('error');
                last_nameID.addClass('error').attr('placeholder', 'Please Enter Last Name');
            } else {
                last_nameID.removeClass('error');
            }

            if (!subject) {
                subjectID.removeClass('error');
                subjectID.addClass('error').attr('placeholder', 'Please Enter Subject')
            } else {
                subjectID.removeClass('error');
            }
            if (!email) {
                emailID.removeClass('error');
                emailID.addClass('error').attr('placeholder', 'Please Enter Email')
            } else {
                emailID.removeClass('error');
            }
            if (!message) {
                messageID.removeClass('error');
                messageID.addClass('error').attr('placeholder', 'Please Enter Your Message')
            } else {
                messageID.removeClass('error');
            }


            if (email && message && last_name && subject && first_name) {
                $.ajax({
                    type: "POST",
                    url: 'contact.php',
                    data: {
                        'last_name': last_name,
                        'subject': subject,
                        'first_name': first_name,
                        'email': email,
                        'message': message,
                    },
                    success: function (data) {
                        formID.children('.email-success').remove();
                        formID.prepend('<span class="alert alert-success email-success">' + data + '</span>');
                        last_nameID.val('');
                        first_nameID.val('');
                        subjectID.val('');
                        emailID.val('');
                        messageID.val('');
                        $('.email-success').fadeOut(5000);
                    }
                });
            } else {
                formID.children('.email-success').remove();
                formID.prepend('<span class="alert alert-danger email-success">Somenthing is wrong</span>');
                $('.email-success').fadeOut(5000);
            }
        });

    });
    //define variable for store last scrolltop
    var lastScrollTop = '';
    $(window).on('scroll', function () {
        //back to top show/hide
        var ScrollTop = $('.back-to-top');
        if ($(window).scrollTop() > 1000) {
            ScrollTop.fadeIn(1000);
        } else {
            ScrollTop.fadeOut(1000);
        }
        /*--------------------------
         sticky menu activation
        -------------------------*/
        var st = $(this).scrollTop();
        var mainMenuTop = $('.navbar-area');
        if ($(window).scrollTop() > 115) {
            if (st > lastScrollTop) {
                // hide sticky menu on scrolldown 
                mainMenuTop.removeClass('nav-fixed');

            } else {
                // active sticky menu on scrollup 
                mainMenuTop.addClass('nav-fixed');
            }

        } else {
            mainMenuTop.removeClass('nav-fixed ');
        }
        lastScrollTop = st;

    });

    $(window).on('load', function () {
        /*-----------------
            preloader
        ------------------*/
        var preLoder = $("#preloader");
        preLoder.fadeOut(1000);
        /*-----------------
            back to top
        ------------------*/
        var backtoTop = $('.back-to-top')
        backtoTop.fadeOut(100);
    });

}(jQuery));


// buy battry in shop

var ShopBattryNum = 0;

var shopNum = document.getElementById('ShopNum');

var ShopPro1 = document.getElementById('shopProducts1');

var ShopPro2 = document.getElementById('shopProducts2');

var ShopPro3 = document.getElementById('shopProducts3');

function buyingInShop() {


    ShopBattryNum += 1;


    shopNum.innerHTML = ShopBattryNum


    if (ShopBattryNum === 1) {

        ShopPro1.style.display = "block";

    } else if (ShopBattryNum === 2) {

        ShopPro2.style.display = "block";

    }
    else if (ShopBattryNum === 3) {

        ShopPro3.style.display = "block";

    }

}

function deleteINShop() {

    if (ShopBattryNum > 0) {
        ShopBattryNum -= 1;
    } else {
        ShopBattryNum = 0;
    }

    shopNum.innerHTML = ShopBattryNum

}

// cart

var CartNumbuy = 3

var cartProducts1 = document.getElementById('cartProducts1');

var cartProducts2 = document.getElementById('cartProducts2');

var cartProducts3 = document.getElementById('cartProducts3');

///////////////////

var CartListPro1 = document.getElementById('CartListPro1');

var CartListPro2 = document.getElementById('CartListPro2');

var CartListPro3 = document.getElementById('CartListPro3');

var Cartnum = document.getElementById('Cartnum');

function deleteInCart() {


    if (CartNumbuy === 3) {

        cartProducts1.style.display = "none"

        CartListPro1.style.display = "none"

    } else if (CartNumbuy === 2) {

        cartProducts2.style.display = "none"

        CartListPro2.style.display = "none"

    } else if (CartNumbuy === 1) {

        cartProducts3.style.display = "none"

        CartListPro3.style.display = "none"
    }

    if (CartNumbuy > 0) {

        CartNumbuy -= 1;

    } else {

        CartNumbuy = 0;

    }

    Cartnum.innerHTML = CartNumbuy;

}


/* Set the width of the side navigation to 250px and the left margin of the page content to 250px and add a black background color to body */
function openNav() {
    document.getElementById("mySidenav").style.width = "100%";
}

/* Set the width of the side navigation to 0 and the left margin of the page content to 0, and the background color of body to white */
function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
    document.getElementById("main").style.marginLeft = "0";
    document.body.style.backgroundColor = "white";
}
