// js for main slider

$(document).ready(function () {

    // looping slider
    var loop = function () {
        var i;
        var slides = document.getElementsByClassName("slide-container");
        var indicators = document.getElementsByClassName("indicator");

        slideIndex++;
        if (slideIndex > slides.length) { slideIndex = 1 }

        // hide all slides
        for (i = 0; i < slides.length; i++) {
            slides[i].style.display = "none";
        }

        // deactivate all indicators
        for (i = 0; i < indicators.length; i++) {
            indicators[i].className = indicators[i].className.replace(" active", "");
        }

        // show current slide 
        slides[slideIndex - 1].style.display = "block";

        // activate current indicator
        indicators[slideIndex - 1].className += " active";
    };

    var slideIndex = 0;
    sliderLoop = setInterval(loop, 4000);

    $('#downButton').click(function () {
        clearInterval(sliderLoop);

        setTimeout(function () {
            var i;
            var slides = document.getElementsByClassName("slide-container");
            var indicators = document.getElementsByClassName("indicator");

            slideIndex++;
            if (slideIndex > slides.length) { slideIndex = 1 }
            if (slideIndex < 1) { slideIndex = slides.length }


            // hide all slides
            for (i = 0; i < slides.length; i++) {
                slides[i].style.display = "none";
            }

            // deactivate all indicators
            for (i = 0; i < indicators.length; i++) {
                indicators[i].className = indicators[i].className.replace(" active", "");
            }

            // show current slide 
            slides[slideIndex - 1].style.display = "block";

            // activate current indicator
            indicators[slideIndex - 1].className += " active";

        }, 0);

        sliderLoop = setInterval(loop, 5000);
    }); // rightButton

    $('#upButton').click(function () {
        clearInterval(sliderLoop);

        setTimeout(function () {
            var i;
            var slides = document.getElementsByClassName("slide-container");
            var indicators = document.getElementsByClassName("indicator");

            slideIndex--;
            if (slideIndex > slides.length) { slideIndex = 1 }
            if (slideIndex < 1) { slideIndex = slides.length }

            // hide all slides
            for (i = 0; i < slides.length; i++) {
                slides[i].style.display = "none";
            }

            // deactivate all indicators
            for (i = 0; i < indicators.length; i++) {
                indicators[i].className = indicators[i].className.replace(" active", "");
            }

            // show current slide 
            slides[slideIndex - 1].style.display = "block";

            // activate current indicator
            indicators[slideIndex - 1].className += " active";
        }, 0);
        
        sliderLoop = setInterval(loop, 5000);
    }); // leftButton
});