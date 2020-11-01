var geocoder;
var map;
var address = "new york city";

function initMap() {

    map = new google.maps.Map(document.getElementById('map-canvas'), {
        center: { lat: 50.641111, lng: 4.668056 },
        zoom: 7
    });
    //geocoder = new google.maps.Geocoder();
    //codeAddress(geocoder, map);

    var markerGrandPlace = new google.maps.Marker({
        position: new google.maps.LatLng(50.846759, 4.352446),
        map: map,
        title: "Brussels Grand-Place"
    });

    google.maps.event.addListener(markerGrandPlace, "click", function () {
        map.panTo(this.getPosition());
        map.setZoom(20);
    });

    //var markerGrandPlace = new google.maps.Marker({
    //    position: new google.maps.LatLng(50.846759, 4.352446),
    //    map: map,
    //    title: "Brussels Grand-Place",
    //    icon: "http://maps.google.com/mapfiles/ms/icons/blue-dot.png"
    //});
}
var responsiveZoom = (window.innerWidth < 768) ? 6.75 : 7.75;

window.addEventListener("resize", function () {
    if (window.innerWidth < 768) responsiveZoom = 6.75
    else if (window.innerWidth > 768) responsiveZoom = 7.75
    map.setZoom(responsiveZoom);
});

//////////////////////


function codeAddress(geocoder, map) {
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status === 'OK') {
            map.setCenter(results[0].geometry.location);
            var marker = new google.maps.Marker({
                map: map,
                position: results[0].geometry.location
            });
        } else {
            alert('Geocode was not successful for the following reason: ' + status);
        }
    });
}