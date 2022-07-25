export function initialMap(identifier,lat,lng, dotNetHelper) {
    window.componentref = dotNetHelper;

    if (!lat || !lng) {
        if (!window.userlatitude || !window.userlongitude) {
            window.userlatitude = 33;
            window.userlongitude = 33;
        }
    } else {
        window.userlatitude = lat;
        window.userlongitude = lng;
    }
    
   
    window.mymap = L.map(identifier).setView([userlatitude, userlongitude], 16);

    L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1IjoibWV5c2FtYWZhciIsImEiOiJja2ZvZWswNjgwMGRuMnFyNWJ6cnhucGRoIn0.ppgyfSQrNUcgkJ9XWAIndw', {
        /*attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',*/
        maxZoom: 18,
        id: 'mapbox/streets-v11',
        tileSize: 512,
        zoomOffset: -1,
        accessToken: 'your.mapbox.access.token'
    }).addTo(mymap);

    window.marker = L.marker([userlatitude, userlongitude]).addTo(mymap);
    window.center = mymap.getCenter();

    mymap.on('dragend', ondragend);
    mymap.on('move', onmove);
}
export function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
    } else {
        x.innerHTML = "Geolocation is not supported by this browser.";
    }
}
function showPosition(position) {
    window.userlatitude = position.coords.latitude;
    window.userlongitude = position.coords.longitude;

}

function ondragend() {
    var LatLng = marker.getLatLng();
    var request = new XMLHttpRequest();
    var url = 'https://nominatim.openstreetmap.org/reverse?lat=' + LatLng.lat + '&lon=' + LatLng.lng + '&format=json&accept-language=fa';
    // Open a new connection, using the GET request on the URL endpoint
    request.open('GET', url, true);

    request.onload = function () {
        var data = JSON.parse(this.response)

        if (request.status >= 200 && request.status < 400) {
            componentref.invokeMethodAsync('OnDragEnd', data.display_name, LatLng.lat, LatLng.lng);           

        } else {
            console.log('error')
        }

    };

    // Send request
    request.send();
}
//function onmoveend() {
//    console.log(marker.getLatLng());

//}
function onmove() {
    center = mymap.getCenter();
    marker.setLatLng([center.lat, center.lng]);

}