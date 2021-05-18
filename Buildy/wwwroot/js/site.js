// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {

    $("#viewAllProjects").on("click", function () {

        $(".more-project").slideDown(400, "swing", function () {
            var distanceFromTopPlusProjects = $(document).height() + 580;
            $('html, body').animate({ scrollTop: distanceFromTopPlusProjects }, 400);
        });

        $(this).hide();
        $("#viewLessProjects").show();

        
    });

    $("#viewLessProjects").on("click", function () {

        $(".more-project").slideUp();

        $(this).hide();
        $("#viewAllProjects").show();
    });
});