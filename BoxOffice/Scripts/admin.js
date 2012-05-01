// GLOBALS
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
                success: function(data) {
                    response($.map(data, function(item) {
                        return { label: item.Name, value: item.Name };
                    }));
                }
            });
        },
        minLength: 1, // require at least one character from the user
        select: function (event, ui) {
            alert(event.item);
        }
    });

    var $container = $(".container");
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

function deleteComment(id) {
    $.ajax({
        url: '/Admin/DeleteComment',
        type: "GET",
        dataType: "json",

        // query will be the param used by your action method
        data: { id: id },
        success: function (data) {
            var sel = "#deleteComment" + id;
            var $selector = $(sel);
            if (data.success) {
                $($selector).animate({
                    backgroundColor: "#008000"
                }, 500, function () {
                    $($selector).animate({
                        backgroundColor: "#063559"
                    }, 500);
                });
                window.location.href = "/Admin/Index";
            } else {
                $($selector).animate({
                    backgroundColor: "#FF0000"
                }, 500, function () {
                    $($selector).animate({
                        backgroundColor: "#063559"
                    }, 500);
                });
            }
        }
    });
}

function deleteUser(id) {
    $.ajax({
        url: '/Admin/DeleteUser',
        type: "GET",
        dataType: "json",

        // query will be the param used by your action method
        data: { id: id },
        success: function (data) {
            var sel = "#deleteUser" + id;
            var $selector = $(sel);
            if (data.success) {
                $($selector).animate({
                    backgroundColor: "#008000"
                }, 500, function () {
                    $($selector).animate({
                        backgroundColor: "#063559"
                    }, 500);
                });
                window.location.href = "/Admin/Index";
            } else {
                $($selector).animate({
                    backgroundColor: "#FF0000"
                }, 500, function () {
                    $($selector).animate({
                        backgroundColor: "#063559"
                    }, 500);
                });
            }
        }
    });
}

function promoteMovie(id) {
    $.ajax({
        url: '/Admin/Promote/',
        type: "GET",
        dataType: "json",
        data: {
          id: id  
        },
        success: function (data) {
            var sel = "#promoteButton" + id;
            if (data.success) {
                $(sel).animate({
                    backgroundColor: "#008000"
                }, 500, function () {
                    $(sel).animate({
                        backgroundColor: "#063559"
                    }, 500);
                });
            } else {
                $(sel).animate({
                    backgroundColor: "#FF0000"
                }, 500, function () {
                    $(sel).animate({
                        backgroundColor: "#063559"
                    }, 500);
                });
            }
        },
        error: function (data) {
            var selector = "#promoteButton" + id;
            $(selector).animate({
                backgroundColor: '#FF0000'
            }, 500);
            $(selector).animate({
                backgroundColor: '#063559'
            }, 500);
        }
    });
}

function deleteMovie(id) {
    $.ajax({
        url: '/Admin/Delete/',
        type: "GET",
        dataType: "json",
        data: {
            id: id
        },
        success: function (data) {
            var sel = "#deleteButton" + id;
            if (data.success) {
                $(sel).animate({
                    backgroundColor: "#008000"
                }, 500, function () {
                    $(sel).animate({
                        backgroundColor: "#063559"
                    }, 500);
                });
            } else {
                $(sel).animate({
                    backgroundColor: "#FF0000"
                }, 500, function () {
                    $(sel).animate({
                        backgroundColor: "#063559"
                    }, 500);
                });
            }
        },
        error: function (data) {
            var selector = "#deleteButton" + id;
            $(selector).animate({
                backgroundColor: '#FF0000'
            }, 500);
            $(selector).animate({
                backgroundColor: '#063559'
            }, 500);
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




