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

    //$('#smartwizard').smartWizard({

     
    //});




    $('#wizard').smartWizard({
        labelNext: 'Next', // label for Next button
        labelPrevious: 'Previous', // label for Previous button
        labelFinish: 'Finish',  // label for Finish button  
        buttonOrder: ['finish', 'next', 'prev'],
        enableFinishButton: false,
        //onShowStep: showAStepCallback,
        //onFinish: onFinishCallback,
        onLeaveStep: function (from, to) {
            //do nothing
            alert('leavetest')
            return true;
        }, // triggers when leaving a step
        onShowStep: alert('test'),  // triggers when showing a step
        onFinish: function () {
            alert('testnew');
        },  // triggers when Finish button is clicked  
        buttonOrder: ['finish', 'next', 'prev']  // button order, to hide a button remove it from the list
    });

    $('.btn.disabled.sw-btn-next').text('Place Order');

    $(".btn.sw-btn-next").click(function () {
        $('.btn.disabled.sw-btn-next').text('Place Order');

    });

    $(".btn.sw-btn-prev").click(function () {
        $('.btn.sw-btn-next').text('Next');

    });


   



    

    

});