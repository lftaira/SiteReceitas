// When the user scrolls the page, execute myFunction
window.onscroll = function () {
  myFunction();
};

var pagina = window.location.href;

// Get the navbar
var navbar = document.getElementById("navbar");
var botaonav = document.getElementById("botaonav")
// Get the offset position of the navbar
var sticky = navbar.offsetTop;

function myFunction() {
  
    if (window.location.href.length <= 28 && window.pageYOffset > sticky) {
      console.log("teste");
      navbar.classList.add("sticky");
      $(".navbar-fixed-top").addClass("opaque");
    } else {
      navbar.classList.remove("sticky");
      $(".navbar-fixed-top").removeClass("opaque");
    }
  }

if (pagina.length >= 31) {
  $(".navbar")
    .removeClass("bg-transparent")
    .removeClass("navbar-fixed-top")
    .addClass("navbar-solid");
  document.getElementById("mainid").classList.addClass("cor-texto");
}

var clicado = false;
$(document).ready(function() {
  $('#botaonav').on('click', function() {
    if (clicado)
    {
      $(".navbar").removeClass("navbar-solid").addClass("bg-transparent").addClass("navbar-fixed-top");
      clicado = false;
    }else{
      $(".navbar").removeClass("bg-transparent").removeClass("navbar-fixed-top").addClass("navbar-solid");
      clicado = true;
    }
  });
});


