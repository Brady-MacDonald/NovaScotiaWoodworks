// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.



function snackbarFunction() {
    var snack = document.getElementById("snackbar");

    snack.classList.add("show");

    setTimeout(function () { snack.className = snack.className.replace("show", ""); }, 5000);
}




$(window).scroll(function (e) {
    var $el = $('.fixedElement');
    var isPositionFixed = ($el.css('position') == 'fixed');
    if ($(this).scrollTop() > 200 && !isPositionFixed) {
        $el.css({ 'position': 'fixed', 'top': '0px' });
    }
    if ($(this).scrollTop() < 200 && isPositionFixed) {
        $el.css({ 'position': 'static', 'top': '0px' });
    }
});



// When the user scrolls the page, execute myFunction
window.onscroll = function () { scrollFunction() };

// Get the navbar
var navbar = document.getElementById("navbar");
//var customeOrder = document.getElementById("test");

// Get the offset position of the navbar
var sticky = navbar.offsetTop;
var stickyOrder = customeOrder.offsetTop;

// Add the sticky class to the navbar when you reach its scroll position. Remove "sticky" when you leave the scroll position
function scrollFunction() {
    if (window.pageYOffset >= sticky) {
        navbar.classList.add("sticky")
    } else {
        navbar.classList.remove("sticky");
    }
    if (window.pageYOffset >= stickyOrder) {
        customeOrder.classList.add("stickyOrder")
    } else {
        customeOrder.classList.remove("stickyOrder");
    }
}


// When the user scrolls the page, execute myFunction
//window.onscroll = function () { scrollFunction1() };

// Get the navbar

// Get the offset position of the navbar
var sticky1 = navbar1.offsetTop;

// Add the sticky class to the navbar when you reach its scroll position. Remove "sticky" when you leave the scroll position
function scrollFunction1() {
    if (window.pageYOffset >= sticky1) {
        navbar1.classList.add("sticky1")
    } else {
        navbar1.classList.remove("sticky1");
    }
}