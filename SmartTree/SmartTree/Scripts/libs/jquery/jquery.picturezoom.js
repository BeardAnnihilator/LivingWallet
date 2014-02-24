$(document).ready(function(){


    $('.small-image').click(function (e) {
            $('.big-image').hide();

            if (this.id == "2b-1") {
                $('.big-image#2b-l').attr("src", "/Content/img/razors/T-for-two.jpg");
                $('#2bladeZoom').zoom({url : "/Content/img/razors/T-for-two-big.png"});
            } else if (this.id == "2b-2") {
                $('.big-image#2b-l').attr("src", "/Content/img/razors/packshot-2.jpg");
                $('#2bladeZoom').zoom({url : "/Content/img/razors/packshot-2-big.png"});
            } else if (this.id == "2b-3") {
                $('.big-image#2b-l').attr("src", "/Content/img/razors/2blades.jpg");
                $('#2bladeZoom').zoom({url : "/Content/img/razors/2blades-big.png"});
            } else if (this.id == "3b-1") {
                $('.big-image#3b-l').attr("src", "/Content/img/razors/Trois-freres.jpg");
                $('#3bladeZoom').zoom({url : "/Content/img/razors/Trois-freres-big.png"});
            } else if (this.id == "3b-2") {
                $('.big-image#3b-l').attr("src", "/Content/img/razors/packshot-3.jpg");
                $('#3bladeZoom').zoom({url : "/Content/img/razors/packshot-3-big.png"});
            } else if (this.id == "3b-3") {
                $('.big-image#3b-l').attr("src", "/Content/img/razors/3blades.jpg");
                $('#3bladeZoom').zoom({url : "/Content/img/razors/3blades-big.png"});
            } else if (this.id == "5b-1") {
                $('.big-image#5b-l').attr("src", "/Content/img/razors/Le-terminator.jpg");
                $('#5bladeZoom').zoom({url : "/Content/img/razors/Le-terminator-big.png"});
            } else if (this.id == "5b-2") {
                $('.big-image#5b-l').attr("src", "/Content/img/razors/packshot-5.jpg");
                $('#5bladeZoom').zoom({url : "/Content/img/razors/packshot-5-big.png"});
            } else if (this.id == "5b-3") {
                $('.big-image#5b-l').attr("src", "/Content/img/razors/5blades.jpg");
                $('#5bladeZoom').zoom({url : "/Content/img/razors/5blades-big.png"});
            } else if (this.id == "women-1") {
                $('.big-image#women-l').attr("src", "/Content/img/razors/Le-wonder-woman.jpg");
                $('#womenZoom').zoom({url : "/Content/img/razors/Le-wonder-woman-big.png"});
            } else if (this.id == "women-2") {
                $('.big-image#women-l').attr("src", "/Content/img/razors/packshot-women.jpg");
                $('#womenZoom').zoom({url : "/Content/img/razors/packshot-women-big.png"});
            } else if (this.id == "women-3") {
                $('.big-image#women-l').attr("src", "/Content/img/razors/women.jpg");
                $('#womenZoom').zoom({url : "/Content/img/razors/women-big.png"});
            }
            $('.big-image').fadeIn('slow');

    });


    });