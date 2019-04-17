$('.owl-carousel').owlCarousel({
    loop: true,
    margin: 20,
    nav: true,
    autoplay: true,
    autoplayTimeout: 5000,
    autoplayHoverPause: true,
    responsive: {
        0: {
            items: 1
        },
        600: {
            items: 3
        },
        1000: {
            items: 4
        }
    }
});


$(document).ready(function () {

    $('#smartwizard').smartWizard({

     
    });

    $('.btn.disabled.sw-btn-next').text('Add to cart');

    $(".btn.sw-btn-next").click(function () {
        $('.btn.disabled.sw-btn-next').text('Add to cart');

    });

    $(".btn.sw-btn-prev").click(function () {
        $('.btn.sw-btn-next').text('Next');

    });


    $('.btn.disabled.sw-btn-next').click(function () {
        alert("add to cart btn clicked");

    });


    

});