// When the user scrolls the page, execute myFunction
window.onscroll = function() {myFunction()};


// Get the navbar
var navbar = document.getElementById("navbar");

// Get the offset position of the navbar
var sticky = navbar.offsetTop;

// Add the sticky class to the navbar when you reach its scroll position. Remove "sticky" when you leave the scroll position
function myFunction() {
  if (window.pageYOffset > sticky) {
    navbar.classList.add("sticky")
    $('.navbar-fixed-top').addClass('opaque');
  } else {
    navbar.classList.remove("sticky");
    $('.navbar-fixed-top').removeClass('opaque');

  }
}

$(window).onscroll(function() {
    if ($(this).scrollTop() > 0.1) {
    }else{
    }
})