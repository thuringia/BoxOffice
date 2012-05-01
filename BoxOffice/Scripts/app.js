// GLOBALS
var searchFieldVisible = false;
var rateFieldVisible = false;
var commentFieldVisible = false;

// $(document).ready()
$(document).ready(function () {
    $("#searchTerm").autocomplete({
        source: function (request, response) {
            // define a function to call your Action (assuming UserController)
            $.ajax({
                url: 'Movies/ajaxSearch',
                type: "GET",
                dataType: "json",

                // query will be the param used by your action method
                data: { q: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item, value: item };
                    }));
                }
            });
        },
        minLength: 1, // require at least one character from the user
    });

    // accordion for queue
    var $accordion = $("#accordion");
    if ($accordion) {
        $("#accordion")
			.accordion({
			    header: "> div > h3",
			    collapsible: true,
			    active: false,
			    autoHeight: false
			})
			.sortable({
			    axis: "y",
			    handle: "h3",
			    stop: function (event, ui) {
			        // IE doesn't register the blur when sorting
			        // so trigger focusout handlers to remove .ui-state-focus
			        ui.item.children("h3").triggerHandler("focusout");
			    }
			});
        $('.ui-accordion').bind('accordionchange', function (event, ui) {
            var newHeader = ui.newHeader; // jQuery object, activated header
            var oldHeader = ui.oldHeader;// jQuery object, previous header
        });
    }

    // view masonry
    var $container = $(".containerView");
    if ($container) {
        $container.masonry({
            itemSelector: '.item',
            gutterWidth: 0,
            isAnimated: !Modernizr.csstransitions,
            animationOptions: {
                duration: 750,
                easing: 'linear',
                queue: false
            }
        });
    }

    // queue masonry
    var $container = $(".containerQueue");
    if ($container) {
        $container.masonry({
            itemSelector: '.item',
            containerWidth: function (containerWidth) {
                return containerWidth / 2;
            },
            gutterWidth: 10,
            isAnimated: !Modernizr.csstransitions,
            animationOptions: {
                duration: 750,
                easing: 'linear',
                queue: false
            }
        });
    }

    var $container = $(".container");
    if ($container) {
        $container.imagesLoaded(function () {
            $container.masonry({
                itemSelector: '.tile',
                columnWidth: function (containerWidth) {
                    return containerWidth / 4;
                },
                gutterWidth: 0,
                isAnimated: !Modernizr.csstransitions,
                animationOptions: {
                    duration: 750,
                    easing: 'linear',
                    queue: false
                }
            });
        });
    }

    $containerMovie = $(".containerMovie");
    if ($containerMovie) {
        $containerMovie.imagesLoaded(function () {
            $containerMovie.masonry({
                itemSelector: '.item',
                gutterWidth: 50,
                isAnimated: !Modernizr.csstransitions,
                animationOptions: {
                    duration: 750,
                    easing: 'linear',
                    queue: false
                }
            });
        });
    }
});

function returnDVD(id) {
    $.ajax({
        url: '/Users/Return',
        type: "GET",
        dataType: "json",

        // query will be the param used by your action method
        data: { id: id },
        success: function (data) {
            var sel = "#return" + id;
            var $selector = $(sel);
            if (data.success) {
                $($selector).animate({
                    backgroundColor: "#008000"
                }, 500, function () {
                    $($selector).animate({
                        backgroundColor: "#063559"
                    }, 500);
                });
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

function flagComment(id) {
    $.ajax({
        url: '/Movies/Flag',
        type: "GET",
        dataType: "json",

        // query will be the param used by your action method
        data: { id: id },
        success: function (data) {
            var sel = ".comment" + id;
            var $selector = $(sel);
            if (data.success) {
                $($selector).animate({
                    backgroundColor: "#008000"
                }, 500, function () {
                    $($selector).animate({
                        backgroundColor: "rgba(0, 0, 0, 0.0)"
                    }, 500);
                });
            } else {
                $($selector).animate({
                    backgroundColor: "#FF0000"
                }, 500, function () {
                    $($selector).animate({
                        backgroundColor: "rgba(0, 0, 0, 0.0)"
                    }, 500);
                });
            }
        }
    });
}

function unqueue(id) {
    $.ajax({
        url: '/Users/Unqueue',
        type: "GET",
        dataType: "json",

        // query will be the param used by your action method
        data: { id: id },
        success: function (data) {
            var sel = "#unqueueButton";
            var $selector = $(sel);
            if (data.success) {
                $($selector).animate({
                    backgroundColor: "#008000"
                }, 500, function () {
                    $($selector).animate({
                        backgroundColor: "#063559"
                    }, 500);
                });
                window.location.href = "/Users/Queue";
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

function showComment() {
    var $selector = $("#commentField");
    if (commentFieldVisible == false) {
        commentFieldVisible = true;
        // make search fiel visible
        $selector.removeClass("hidden");
        $selector.addClass("visible");
        $selector.animate(
            {
                opacity: 1
            }, 500, function () {
            }
            );
    }
    else {
        // hide search field
        commentFieldVisible = false;
        $("#searchField").animate(
            {
                opacity: 0
            }, 500, function () {
                $selector.removeClass("visible");
                $selector.addClass("hidden");
            });
    }
}

function sendComment(id) {
    $.ajax({
        url: '/Movies/Comment',
        type: "GET",
        dataType: "json",

        // query will be the param used by your action method
        data: { id: id, comment: $("#commentText").val() },
        success: function (data) {
            if (data.success) {
                $("#comment").animate({
                    backgroundColor: "#008000"
                }, 500, function () {
                    $("#comment").animate({
                        backgroundColor: "#063559"
                    }, 500);
                });
            } else {
                $("#comment").animate({
                    backgroundColor: "#FF0000"
                }, 500, function () {
                    $("#comment").animate({
                        backgroundColor: "#063559"
                    }, 500);
                });
            }
        }
    });
}

function rate(id) {
    if (rateFieldVisible == false) {
        rateFieldVisible = true;
        $("#ratingField").removeClass("hidden");
        $("#ratingField").addClass("visible");
        $("#ratingField").animate(
            {
                opacity: 1
            }, 500, function () {
                $('#ratingStars').raty({
                    half: true,
                    halfShow: true,
                    number: 10,
                    path: "/Content/img",
                    click: function (score, evt) {
                        $.ajax({
                            url: '/Movies/Rate',
                            type: "GET",
                            dataType: "json",

                            // query will be the param used by your action method
                            data: { id: id, rating: score },
                            success: function (data) {
                                if (data.success) {
                                    $("#rate").animate({
                                        backgroundColor: "#008000"
                                    }, 500, function() {
                                        $("#rate").animate({
                                            backgroundColor: "#063559"
                                        }, 500);
                                    });
                                } else {
                                    $("#rate").animate({
                                        backgroundColor: "#FF0000"
                                    }, 500, function () {
                                        $("#rate").animate({
                                            backgroundColor: "#063559"
                                        }, 500);
                                    });
                                }
                            }
                        });
                    }
                });
            }
            );
    } else {
        $("#ratingField").animate({
            opacity: 0
        }, 500, function() {
            $("ratingField").removeClass("visible");
            $("ratingField").addClass("hidden");
            rateFieldVisible = false;
        });
    }


}

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

function addToQueue(id) {
    $.ajax({
        url: '/Movies/Rent',
        type: "GET",
        dataType: "json",

        // query will be the param used by your action method
        data: { id: id },
        success: function (data) {
            if (data.success) {
                $("#addToQueue").animate({
                    backgroundColor: green
                }, 500, function () {
                    $("#addToQueue").animate({
                        backgroundColor: "#063559"
                    }, 500, function () {

                    });
                });
            } else if (data.fail) {
                $("#addToQueue").animate({
                    backgroundColor: red
                }, 500, function () {
                    $("#addToQueue").animate({
                        backgroundColor: "#063559"
                    }, 500, function () {

                    });
                });
            }

        }
    });
}



