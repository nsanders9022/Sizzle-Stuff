
function replaceSlashes(string) {
    var splitString = string.split('');

    for (i = 0; i < string.length; i++) {
        if (splitString[i] === "\\") {
            splitString.splice(i, 1, "/");
        }
    }

    var outputString = splitString.join('');
    return outputString;
}

function updateImageSize() {
    $(".background-cover-image").each(function(){
        var ratio_cont = jQuery(this).width()/jQuery(this).height();
        var $img = jQuery(this).find("img");
        var ratio_img = $img.width()/$img.height();
        if (ratio_cont > ratio_img)
        {
            $img.css({"width": "100%", "height": "auto"});
        }
        else if (ratio_cont < ratio_img)
        {
            $img.css({"width": "auto", "height": "100%"});
        }
    })
    $( ".background-cover-image" ).each(function() {
        var $img = $( this ).children("img");
        var $imgUrl = $img.attr("src");
        // console.log('img' + $img);
         console.log('imgUrl' + $imgUrl);
         $imgUrl = replaceSlashes($imgUrl);
         console.log($imgUrl);
        $img.css("opacity", 0);
        $( this ).css({"background-image": "url(" + $imgUrl + ")", "background-size": "cover", "background-position": "center"});
    });
};

$(function(){
    $('.carousel').carousel();

    $('a.left').click(function(){
        $('#photo-carousel').carousel('prev');
    })

    $('a.right').click(function(){
        $('#photo-carousel').carousel('next');
    })

    $(window).scroll(function () {
        //if you hard code, then use console
        //.log to determine when you want the
        //nav bar to stick.
        console.log($(window).scrollTop())
        if ($(window).scrollTop() > 200) {
            $('#my-nav').addClass('navbar-fixed-top');
        }
        if ($(window).scrollTop() < 200) {
            $('#my-nav').removeClass('navbar-fixed-top');
        }
    });

    updateImageSize();
    $(window).resize(function() {
        updateImageSize();
    });

    $("#product-table").tablesorter();
    $('.collapse').collapse();

// Button hide/showing for the managers
    $(".manager-heading").click(function() {
        $(this).next().toggle();
    });

// Button hide/showing on the admin products page
    $(".admin-edit-button").click(function() {
        $(this).parent().parent().next().show();
        $(this).parent().parent().hide();
    });
    $(".cancel-edits").click(function() {
        $(this).parent().parent().prev().show();
        $(this).parent().parent().hide();
    });

// Button hide/showing on the singular admin products page
    $(".single-edit-button").click(function() {
        $(this).hide();
        $(".edit-values-product").show();
        $(".single-cancel-button").show();
    });
    $(".single-cancel-button").click(function() {
        $(this).hide();
        $(".edit-values-product").hide();
        $(".single-edit-button").show();
    });

    if ($('.rating-value').length){
        var rating = $('.rating-value').val();
        if (rating == 5) {
            $('#star5').attr('checked', true);
        } else if (rating == 4) {
            $('#star4').attr('checked', true);
        } else if (rating == 3) {
            $('#star3').attr('checked', true);
        } else if (rating == 2) {
            $('#star2').attr('checked', true);
        } else if (rating == 1) {
            $('#star1').attr('checked', true);
        }
    }

});
