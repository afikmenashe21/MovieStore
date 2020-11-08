var map, infobox;
var usersAddress = [];
var userNames = [];

function GetMap() {
    map = new Microsoft.Maps.Map('#map-canvas', {});

    //Create an infobox at the center of the map but don't show it.
    infobox = new Microsoft.Maps.Infobox(map.getCenter(), {
        visible: false
    });

    //Assign the infobox to a map instance.
    infobox.setMap(map);

    //Get the data of the user, e.g address and name
    var table = document.getElementById("addressTable");
    for (let i = 0; i < table.children.length; i++) {
        if (i % 2 === 0) {
            usersAddress.push(table.children[i].textContent);
        } else {
            userNames.push(table.children[i].textContent);
        }

    }

    function getData(address, index) {
        Microsoft.Maps.loadModule('Microsoft.Maps.Search', function () {
            var searchManager = new Microsoft.Maps.Search.SearchManager(map);
            var requestOptions = {
                bounds: map.getBounds(),
                where: address,
                callback: function (answer, userData) {
                    map.entities.push(myPushPin(answer.results[0].location, index));
                }
            };
            searchManager.geocode(requestOptions);
        });
    }

    (async function markPins() {
        try {
            for (var i = 0; i < usersAddress.length; i++) {
                var add = usersAddress[i].toString();
                await getData(add, i);
            }
        } catch (err) {
            console.log(err);
        }

    })();

    map.setView({
        center: new Microsoft.Maps.Location(32.109333, 34.855499),
        zoom: 8
    });
}



function myPushPin(location, index) {
    var pin = new Microsoft.Maps.Pushpin(location);
    var name = userNames[index].toString();
    var loc = usersAddress[index].toString();

    //Store some metadata with the pushpin.
    pin.metadata = {
        title: 'User:' + name,
        description: 'Address: ' + loc
    };

    //Add a click event handler to the pushpin.
    Microsoft.Maps.Events.addHandler(pin, 'click', pushpinClicked);

    //Add pushpin to the map.
    map.entities.push(pin);
}

function pushpinClicked(e) {
    //Make sure the infobox has metadata to display.
    if (e.target.metadata) {
        //Set the infobox options with the metadata of the pushpin.
        infobox.setOptions({
            location: e.target.getLocation(),
            title: e.target.metadata.title,
            description: e.target.metadata.description,
            visible: true
        });
    }
}