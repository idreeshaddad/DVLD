// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {

    $("#viewAllProjects").on("click", function () {

        $(".more-project").slideDown();

        $(this).hide();
        $("#viewLessProjects").show();

        
    });

    $("#viewLessProjects").on("click", function () {

        $(".more-project").slideUp();

        $(this).hide();
        $("#viewAllProjects").show();
    });

    // Show/Hide back to top button depending on the scroll position 
    $(window).scroll(function () {

        if ($(this).scrollTop()) {
            $('#scrollToTop').fadeIn();
        } else {
            $('#scrollToTop').fadeOut();
        }
    });

    $("#scrollToTop").click(function () {
        $("html, body").animate({ scrollTop: 0 }, 1000);
    });

    // Start Testimonials slider
    $("#testimonialsSlider").slick({
        arrows: true
    });
});