$(document).ready(function () {
    
    var counter = setInterval(timer, 300); 
    function timer() {
        $(".searchtable").mouseenter(function () {
            $(".searchtable").focus();
        });
        $(".from").mouseenter(function () {
            $(".from").focus();
        });
        $(".to").mouseenter(function () {
            $(".to").focus();
        });
        $(".grids").mouseenter(function () {
            $(".grids").focus();
        });
        $(".amount").mouseenter(function () {
            $(".amount").focus();
        });
    } 
});
