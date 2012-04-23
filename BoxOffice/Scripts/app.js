﻿// GLOBALS
var searchFieldVisible = false;

// $(document).ready()
$(document).ready(function () {
    $("#searchTerm").autocomplete({
    source: function (request, response) {
        // define a function to call your Action (assuming UserController)
        $.ajax({
            url: 'Movies/ajaxSearch', type: "GET", dataType: "json",

            // query will be the param used by your action method
            data: { q: request.term },
            success: function (data) {
                response($.map(data, function (item) {
                    return { label: item, value: item };
                }))
            }
        })
    },
    minLength: 1, // require at least one character from the user
});
});

function showMovieSearch() {
    if (searchFieldVisible == false) {
        searchFieldVisible = true;
        // un-highlight current page
        $(page).removeClass("selected");

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



    