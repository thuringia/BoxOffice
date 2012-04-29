﻿// GLOBALS
var searchFieldVisible = false;

// $(document).ready()
$(document).ready(function () {
    // search for adding a movie
    $("#addSearchTerm").autocomplete({
        source: function (request, response) {
            // define a function to call your Action (assuming UserController)
            $.ajax({
                url: 'http://api.themoviedb.org/2.1/Movie.search/en/json/b0f4c9d847ceda92061d4090b470dc10/',
                type: "GET",
                dataType: "json",

                // query will be the param used by your action method
                data: request.term,
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.Name, value: item.Name };
                    }))
                }
            })
        },
        minLength: 1, // require at least one character from the user
        select: function (event, ui) {
            alert(eve.item);
        }
    });

    var $container = $(".section");
    if ($container) {
        $container.imagesLoaded(function () {
            $container.masonry({
                itemSelector: '.tile',
                gutterWidth: 20,
                isAnimated: !Modernizr.csstransitions,
                animationOptions: {
                    duration: 750,
                    easing: 'linear',
                    queue: false
                }
            });
        });
    }

    var $containerTile = $(".containerTile");
    $containerTile.imagesLoaded(function () {
        $containerTile.masonry({
            itemSelector: '.item',
            gutterWidth: 0,
            isAnimated: !Modernizr.csstransitions,
            animationOptions: {
                duration: 750,
                easing: 'linear',
                queue: false
            }
        });
        $(".buttons").css({ 'position': '' });
    });

});

function promoteMovie(id) {
    $.ajax({
        url: '/Admin/Promote/',
        type: "GET",
        dataType: "json",
        data: {
          id: id  
        },
        success: function(data) {
            response($.map(data, function(item) {
                return { label: item.Name, value: item.Name };
            }))
        }
    });
}

function showMovieSearch() {
    if (searchFieldVisible == false) {
        searchFieldVisible = true;
        // un-highlight current page
        $(page).removeClass("selected");

        // un-highlight sub-page
        $(subpage).removeClass("selected");

        // highlight search
        $("#search").addClass("selected");

        // make search fiel visible
        $("#searchField").removeClass("hidden");
        $("#searchField").addClass("visible");
        $("#searchField").animate(
            {
                opacity: 1
            }, 500, function () {
            }
            );
    }
    else {
        // un-highlight search field
        $("#search").removeClass("selected");

        // re-highlight current page
        $(page).addClass("selected");

        // re-highlight sub-page
        $(subpage).addClass("selected");

        // hide search field
        searchFieldVisible = false;
        $("#searchField").animate(
            {
                opacity: 0
            }, 500, function () {
                $("#searchField").removeClass("visible");
                $("#searchField").addClass("hidden");
            });
    }
}




