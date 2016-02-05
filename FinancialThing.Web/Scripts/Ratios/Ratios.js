function generateRatios() {
    toastr.info("Building ratios", "Please wait...", { timeOut: 1500 });
    $.ajax({
        url: "/RatiosBuilder/BuildRatios",
        method: "POST",
        data: "",
        success: function(data) {
            toastr.success("Ratios have been built", "Success!", { timeOut: 2000 });
        }
    });
}