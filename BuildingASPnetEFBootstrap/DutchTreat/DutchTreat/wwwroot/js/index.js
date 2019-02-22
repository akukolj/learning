$(document).ready(function() {
    var x = 0;
    var s = "";

    console.log("Hello plural");

    var theForm = $("#theForm");
    theForm.hide();

    var button = $("#buyButton");
    button.on("click",
        function() {
            console.log("Buy item");
        });

    var product = $(".product-props li");

    product.on("click",
        function() {
            console.log("you clicked on" + $(this).text());
        });

    var $loginToggle = $("#loggingToggle");
    var $popuform = $(".popup-form");

    $loginToggle.on("click",function() {
            $popuform.toggle(1000);
        });
});
