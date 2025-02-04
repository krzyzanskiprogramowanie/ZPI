﻿var table_type = 0;
var jsonGuestFromDatabase = "";
var jsonTableFromDatabase = "";

function init() {
    var test = document.getElementById("hiddenGuest");
    var guestsFromBackend = document.getElementById("hiddenGuest");
    var helpValue = "";
    var guest = guestsFromBackend.value;
    var counter = 0;
    var arraySize = 0;
    for (var i = 0; i < guest.length; i++) {
        if (guest.charAt(i) == ",") {
            arraySize++;
        }
    }
    var guestListFromBackend = new Array(arraySize);
    for (i = 0; i < guest.length; i++) {
        helpValue += guest.charAt(i);
        if (guest.charAt(i) == ",") {
            guestListFromBackend[counter] = helpValue;
            helpValue = "";
            counter++;
            continue;
        }
    }
    for (var i = 0; i < arraySize; i++) {

        guestListFromBackend[i] = guestListFromBackend[i].replace(",", " ");
    }

    var $ = go.GraphObject.make;
    console.log(test.value);
    // Initialize the main Diagram
    myDiagram =
        $(go.Diagram, "myDiagramDiv",
            {
                allowDragOut: true,  // to myGuests
                allowClipboard: false,
                draggingTool: $(SpecialDraggingTool),
                rotatingTool: $(HorizontalTextRotatingTool),
                "ModelChanged": function (e) {
                    if (e.isTransactionFinished) {
                        document.getElementById("savedModel").textContent = myDiagram.model.toJson();

                    }
                },
                "undoManager.isEnabled": true
            });

    myDiagram.nodeTemplateMap.add("",  // default template, for people
        $(go.Node, "Auto",
            { background: "transparent" },  // in front of all Tables
            // when selected is in foreground layer
            new go.Binding("layerName", "isSelected", function (s) { return s ? "Foreground" : ""; }).ofObject(),
            { locationSpot: go.Spot.Center },
            new go.Binding("location", "loc", go.Point.parse).makeTwoWay(go.Point.stringify),
            new go.Binding("text", "key"),
            { // what to do when a drag-over or a drag-drop occurs on a Node representing a table
                mouseDragEnter: function (e, node, prev) {
                    var dragCopy = node.diagram.toolManager.draggingTool.copiedParts;  // could be copied from palette
                    highlightSeats(node, dragCopy ? dragCopy : node.diagram.selection, true);
                },
                mouseDragLeave: function (e, node, next) {
                    var dragCopy = node.diagram.toolManager.draggingTool.copiedParts;
                    highlightSeats(node, dragCopy ? dragCopy : node.diagram.selection, false);
                },
                mouseDrop: function (e, node) {
                    assignPeopleToSeats(node, node.diagram.selection, e.documentPoint);
                }
            },
            $(go.Shape, "RoundedRectangle", { fill: "#d4c68e", stroke: null }),
            $(go.Panel, "Viewbox",
                { desiredSize: new go.Size(50, 38) },
                $(go.TextBlock, { margin: 2, desiredSize: new go.Size(55, NaN), font: "10pt Montserrat, sans-serif", textAlign: "center", stroke: "#4e6868" },
                    new go.Binding("text", "", function (data) {
                        var s = data.key;
                        if (data.plus) s += " +" + data.plus.toString();
                        return s;

                    }))
            )
        ));

    // Create a seat element at a particular alignment relative to the table.
    function Seat(number, align, focus) {
        if (typeof align === 'string') align = go.Spot.parse(align);
        if (!align || !align.isSpot()) align = go.Spot.Right;
        if (typeof focus === 'string') focus = go.Spot.parse(focus);
        if (!focus || !focus.isSpot()) focus = align.opposite();
        return $(go.Panel, "Spot",
            { name: number.toString(), alignment: align, alignmentFocus: focus },
            $(go.Shape, "Circle",
                { name: "SEATSHAPE", desiredSize: new go.Size(40, 40), fill: "#e3e6ca", stroke: "transparent", strokeWidth: 2 },
                new go.Binding("fill")),
            $(go.TextBlock, number.toString(),
                { font: "10pt Montserrat, sans-serif", stroke: "#4e6868" },
                new go.Binding("angle", "angle", function (n) { return -n; }))
        );
    }

    function tableStyle() {
        return [
            { background: "transparent" },
            { layerName: "Background" },  // behind all Persons
            { locationSpot: go.Spot.Center, locationObjectName: "TABLESHAPE" },
            new go.Binding("location", "loc", go.Point.parse).makeTwoWay(go.Point.stringify),
            { rotatable: true },
            new go.Binding("angle").makeTwoWay(),
            { // what to do when a drag-over or a drag-drop occurs on a Node representing a table
                mouseDragEnter: function (e, node, prev) {
                    var dragCopy = node.diagram.toolManager.draggingTool.copiedParts;  // could be copied from palette
                    highlightSeats(node, dragCopy ? dragCopy : node.diagram.selection, true);
                },
                mouseDragLeave: function (e, node, next) {
                    var dragCopy = node.diagram.toolManager.draggingTool.copiedParts;
                    highlightSeats(node, dragCopy ? dragCopy : node.diagram.selection, false);
                },
                mouseDrop: function (e, node) { assignPeopleToSeats(node, node.diagram.selection, e.documentPoint); }
            }
        ];
    }


    myDiagram.nodeTemplateMap.add("TableR8",
        $(go.Node, "Spot", tableStyle(),
            $(go.Panel, "Spot",
                $(go.Shape, "RoundedRectangle",
                    { name: "TABLESHAPE", desiredSize: new go.Size(160, 60), fill: "#a3d6c6", stroke: null },
                    new go.Binding("desiredSize", "size", go.Size.parse).makeTwoWay(go.Size.stringify),
                    new go.Binding("fill")),
                $(go.TextBlock, { editable: true, font: "bold 11pt Montserrat, sans-serif", stroke: "#4e6868" },
                    new go.Binding("text", "name").makeTwoWay(),
                    new go.Binding("angle", "angle", function (n) { return -n; }))
            ),
            Seat(1, "0.2 0", "0.5 1"),
            Seat(2, "0.5 0", "0.5 1"),
            Seat(3, "0.8 0", "0.5 1"),
            Seat(4, "1 0.5", "0 0.5"),
            Seat(5, "0.8 1", "0.5 0"),
            Seat(6, "0.5 1", "0.5 0"),
            Seat(7, "0.2 1", "0.5 0"),
            Seat(8, "0 0.5", "1 0.5")
        ));
    myDiagram.nodeTemplateMap.add("TableR6",
        $(go.Node, "Spot", tableStyle(),
            $(go.Panel, "Spot",
                $(go.Shape, "RoundedRectangle",
                    { name: "TABLESHAPE", desiredSize: new go.Size(160, 60), fill: "#a3d6c6", stroke: null },
                    new go.Binding("desiredSize", "size", go.Size.parse).makeTwoWay(go.Size.stringify),
                    new go.Binding("fill")),
                $(go.TextBlock, { editable: true, font: "bold 11pt Montserrat, sans-serif", stroke: "#4e6868" },
                    new go.Binding("text", "name").makeTwoWay(),
                    new go.Binding("angle", "angle", function (n) { return -n; }))
            ),
            Seat(1, "0.2 0", "0.5 1"),
            Seat(2, "0.5 0", "0.5 1"),
            Seat(3, "0.8 0", "0.5 1"),
            Seat(4, "0.8 1", "0.5 0"),
            Seat(5, "0.5 1", "0.5 0"),
            Seat(6, "0.2 1", "0.5 0")
        ));
    myDiagram.nodeTemplateMap.add("TableR5",
        $(go.Node, "Spot", tableStyle(),
            $(go.Panel, "Spot",
                $(go.Shape, "RoundedRectangle",
                    { name: "TABLESHAPE", desiredSize: new go.Size(160, 60), fill: "#a3d6c6", stroke: null },
                    new go.Binding("desiredSize", "size", go.Size.parse).makeTwoWay(go.Size.stringify),
                    new go.Binding("fill")),
                $(go.TextBlock, { editable: true, font: "bold 11pt Montserrat, sans-serif", stroke: "#fff" },
                    new go.Binding("text", "name").makeTwoWay(),
                    new go.Binding("angle", "angle", function (n) { return -n; }))
            ),

            Seat(1, "1 0.5", "0 0.5"),
            Seat(2, "0.8 1", "0.5 0"),
            Seat(3, "0.5 1", "0.5 0"),
            Seat(4, "0.2 1", "0.5 0"),
            Seat(5, "0 0.5", "1 0.5")
        ));
    myDiagram.nodeTemplateMap.add("TableL3_R",
        $(go.Node, "Spot", tableStyle(),
            $(go.Panel, "Spot",
                $(go.Shape, "RoundedRectangle",
                    { name: "TABLESHAPE", desiredSize: new go.Size(160, 60), fill: "#a3d6c6", stroke: null },
                    new go.Binding("desiredSize", "size", go.Size.parse).makeTwoWay(go.Size.stringify),
                    new go.Binding("fill")),
                $(go.TextBlock, { editable: true, font: "bold 11pt Montserrat, sans-serif", stroke: "#4e6868" },
                    new go.Binding("text", "name").makeTwoWay(),
                    new go.Binding("angle", "angle", function (n) { return -n; }))
            ),


            Seat(1, "0.8 1", "0.5 0"),
            Seat(2, "0.5 1", "0.5 0"),
            Seat(3, "0.2 1", "0.5 0"),
            Seat(4, "0 0.5", "1 0.5")
        ));

    myDiagram.nodeTemplateMap.add("TableL3_L",
        $(go.Node, "Spot", tableStyle(),
            $(go.Panel, "Spot",
                $(go.Shape, "RoundedRectangle",
                    { name: "TABLESHAPE", desiredSize: new go.Size(160, 60), fill: "#a3d6c6", stroke: null },
                    new go.Binding("desiredSize", "size", go.Size.parse).makeTwoWay(go.Size.stringify),
                    new go.Binding("fill")),
                $(go.TextBlock, { editable: true, font: "bold 11pt Montserrat, sans-serif", stroke: "#4e6868" },
                    new go.Binding("text", "name").makeTwoWay(),
                    new go.Binding("angle", "angle", function (n) { return -n; }))
            ),

            Seat(1, "1 0.5", "0 0.5"),
            Seat(2, "0.8 1", "0.5 0"),
            Seat(3, "0.5 1", "0.5 0"),
            Seat(4, "0.2 1", "0.5 0"),

        ));
    myDiagram.nodeTemplateMap.add("TableR3",
        $(go.Node, "Spot", tableStyle(),
            $(go.Panel, "Spot",
                $(go.Shape, "RoundedRectangle",
                    { name: "TABLESHAPE", desiredSize: new go.Size(160, 60), fill: "#a3d6c6", stroke: null },
                    new go.Binding("desiredSize", "size", go.Size.parse).makeTwoWay(go.Size.stringify),
                    new go.Binding("fill")),
                $(go.TextBlock, { editable: true, font: "bold 11pt Montserrat, sans-serif", stroke: "#4e6868" },
                    new go.Binding("text", "name").makeTwoWay(),
                    new go.Binding("angle", "angle", function (n) { return -n; }))
            ),
            Seat(1, "0.2 0", "0.5 1"),
            Seat(2, "0.5 0", "0.5 1"),
            Seat(3, "0.8 0", "0.5 1")
        ));

    myDiagram.nodeTemplateMap.add("TableC8",
        $(go.Node, "Spot", tableStyle(),
            $(go.Panel, "Spot",
                $(go.Shape, "Circle",
                    { name: "TABLESHAPE", desiredSize: new go.Size(120, 120), fill: "#a3d6c6", stroke: null },
                    new go.Binding("desiredSize", "size", go.Size.parse).makeTwoWay(go.Size.stringify),
                    new go.Binding("fill")),
                $(go.TextBlock, { editable: true, font: "bold 11pt Montserrat, sans-serif", stroke: "#4e6868" },
                    new go.Binding("text", "name").makeTwoWay(),
                    new go.Binding("angle", "angle", function (n) { return -n; }))
            ),
            Seat(1, "0.50 0", "0.5 1"),
            Seat(2, "0.85 0.15", "0.15 0.85"),
            Seat(3, "1 0.5", "0 0.5"),
            Seat(4, "0.85 0.85", "0.15 0.15"),
            Seat(5, "0.50 1", "0.5 0"),
            Seat(6, "0.15 0.85", "0.85 0.15"),
            Seat(7, "0 0.5", "1 0.5"),
            Seat(8, "0.15 0.15", "0.85 0.85")
        ));

    // what to do when a drag-drop occurs in the Diagram's background
    myDiagram.mouseDrop = function (e) {
        // when the selection is dropped in the diagram's background,
        // make sure the selected people no longer belong to any table
        e.diagram.selection.each(function (n) {
            if (isPerson(n)) unassignSeat(n.data);
        });
    };

    // to simulate a "move" from the Palette, the source Node must be deleted.
    myDiagram.addDiagramListener("ExternalObjectsDropped", function (e) {
        // if any Tables were dropped, don't delete from myGuests
        if (!e.subject.any(isTable)) {
            myGuests.commandHandler.deleteSelection();
        }
    });

    // put deleted people back in the myGuests diagram
    myDiagram.addDiagramListener("SelectionDeleted", function (e) {
        // no-op if deleted by myGuests' ExternalObjectsDropped listener
        if (myDiagram.disableSelectionDeleted) return;
        // e.subject is the myDiagram.selection collection
        e.subject.each(function (n) {
            if (isPerson(n)) {
                myGuests.model.addNodeData(myGuests.model.copyNodeData(n.data));
            }
        });
    });



    // initialize the Palette
    myGuests =
        $(go.Diagram, "myGuests",
            {
                layout: $(go.GridLayout,
                    {
                        sorting: go.GridLayout.Ascending  // sort by Node.text value
                    }),
                allowDragOut: true,  // to myDiagram
                allowMove: false
            });

    myGuests.nodeTemplateMap = myDiagram.nodeTemplateMap;

    // specify the contents of the Palette
    
    //Show Guest from database in manager
    var i;
    for (i = 0; i < arraySize; i++) {

        myGuests.model.addNodeData({ key: guestListFromBackend[i] })
    }

    myGuests.model.undoManager = myDiagram.model.undoManager  // shared UndoManager!

    // To simulate a "move" from the Diagram back to the Palette, the source Node must be deleted.
    myGuests.addDiagramListener("ExternalObjectsDropped", function (e) {
        // e.subject is the myGuests.selection collection
        // if the user dragged a Table to the myGuests diagram, cancel the drag
        if (e.subject.any(isTable)) {
            myDiagram.currentTool.doCancel();
            myGuests.currentTool.doCancel();
            return;
        }
        myDiagram.selection.each(function (n) {
            if (isPerson(n)) unassignSeat(n.data);
        });
        myDiagram.disableSelectionDeleted = true;
        myDiagram.commandHandler.deleteSelection();
        myDiagram.disableSelectionDeleted = false;
        myGuests.selection.each(function (n) {
            if (isPerson(n)) unassignSeat(n.data);
        });
    });

    go.AnimationManager.defineAnimationEffect("location",
        function (obj, startValue, endValue, easing, currentTime, duration, animationState) {
            obj.location = new go.Point(easing(currentTime, startValue.x, endValue.x - startValue.x, duration),
                easing(currentTime, startValue.y, endValue.y - startValue.y, duration));
        }
    );
    load();
} // end init

function isPerson(n) { return n !== null && n.category === ""; }

function isTable(n) { return n !== null && n.category !== ""; }

// Highlight the empty and occupied seats at a "Table" Node
function highlightSeats(node, coll, show) {
    if (isPerson(node)) {  // refer to the person's table instead
        node = node.diagram.findNodeForKey(node.data.table);
        if (node === null) return;
    }
    var it = coll.iterator;
    while (it.next()) {
        var n = it.key;
        // if dragging a Table, don't do any highlighting
        if (isTable(n)) return;
    }
    var guests = node.data.guests;
    for (var sit = node.elements; sit.next();) {
        var seat = sit.value;
        if (seat.name) {
            var num = parseFloat(seat.name);
            if (isNaN(num)) continue;
            var seatshape = seat.findObject("SEATSHAPE");
            if (!seatshape) continue;
            if (show) {
                if (guests[seat.name]) {
                    seatshape.stroke = "red";
                } else {
                    seatshape.stroke = "green";
                }
            } else {
                seatshape.stroke = "white";
            }
        }
    }
}

// Given a "Table" Node, assign seats for all of the people in the given collection of Nodes;
// the optional Point argument indicates where the collection of people may have been dropped.
function assignPeopleToSeats(node, coll, pt) {
    if (isPerson(node)) {  // refer to the person's table instead
        node = node.diagram.findNodeForKey(node.data.table);
        if (node === null) return;
    }
    if (coll.any(isTable)) {
        // if dragging a Table, don't allow it to be dropped onto another table
        myDiagram.currentTool.doCancel();
        return;
    }
    // OK -- all Nodes are people, call assignSeat on each person data
    coll.each(function (n) { assignSeat(node, n.data, pt); });
    positionPeopleAtSeats(node);
}

// Given a "Table" Node, assign one guest data to a seat at that table.
// Also handles cases where the guest represents multiple people, because guest.plus > 0.
// This tries to assign the unoccupied seat that is closest to the given point in document coordinates.
function assignSeat(node, guest, pt) {
    if (isPerson(node)) {  // refer to the person's table instead
        node = node.diagram.findNodeForKey(node.data.table);
        if (node === null) return;
    }
    if (guest instanceof go.GraphObject) throw Error("A guest object must not be a GraphObject: " + guest.toString());
    if (!(pt instanceof go.Point)) pt = node.location;

    // in case the guest used to be assigned to a different seat, perhaps at a different table
    unassignSeat(guest);

    var model = node.diagram.model;
    var guests = node.data.guests;
    // iterate over all seats in the Node to find one that is not occupied
    var closestseatname = findClosestUnoccupiedSeat(node, pt);
    if (closestseatname) {
        model.setDataProperty(guests, closestseatname, guest.key);
        model.setDataProperty(guest, "table", node.data.key);
        model.setDataProperty(guest, "seat", parseFloat(closestseatname));
    }

    var plus = guest.plus;
    if (plus) {  // represents several people
        // forget the "plus" info, since next we create N copies of the node/data
        guest.plus = undefined;
        model.updateTargetBindings(guest);
        for (var i = 0; i < plus; i++) {
            var copy = model.copyNodeData(guest);
            // don't copy the seat assignment of the first person
            copy.table = undefined;
            copy.seat = undefined;
            model.addNodeData(copy);
            assignSeat(node, copy, pt);
        }
    }
}

// Declare that the guest represented by the data is no longer assigned to a seat at a table.
// If the guest had been at a table, the guest is removed from the table's list of guests.
function unassignSeat(guest) {
    if (guest instanceof go.GraphObject) throw Error("A guest object must not be a GraphObject: " + guest.toString());
    var model = myDiagram.model;
    // remove from any table that the guest is assigned to
    if (guest.table) {
        var table = model.findNodeDataForKey(guest.table);
        if (table) {
            var guests = table.guests;
            if (guests) model.setDataProperty(guests, guest.seat.toString(), undefined);
        }
    }
    model.setDataProperty(guest, "table", undefined);
    model.setDataProperty(guest, "seat", undefined);
}

// Find the name of the unoccupied seat that is closest to the given Point.
// This returns null if no seat is available at this table.
function findClosestUnoccupiedSeat(node, pt) {
    if (isPerson(node)) {  // refer to the person's table instead
        node = node.diagram.findNodeForKey(node.data.table);
        if (node === null) return;
    }
    var guests = node.data.guests;
    var closestseatname = null;
    var closestseatdist = Infinity;
    // iterate over all seats in the Node to find one that is not occupied
    for (var sit = node.elements; sit.next();) {
        var seat = sit.value;
        if (seat.name) {
            var num = parseFloat(seat.name);
            if (isNaN(num)) continue;  // not really a "seat"
            if (guests[seat.name]) continue;  // already assigned
            var seatloc = seat.getDocumentPoint(go.Spot.Center);
            var seatdist = seatloc.distanceSquaredPoint(pt);
            if (seatdist < closestseatdist) {
                closestseatdist = seatdist;
                closestseatname = seat.name;
            }
        }
    }
    return closestseatname;
}

// Position the nodes of all of the guests that are seated at this table
// to be at their corresponding seat elements of the given "Table" Node.
function positionPeopleAtSeats(node) {
    if (isPerson(node)) {  // refer to the person's table instead
        node = node.diagram.findNodeForKey(node.data.table);
        if (node === null) return;
    }
    var guests = node.data.guests;
    var model = node.diagram.model;
    for (var seatname in guests) {
        var guestkey = guests[seatname];
        var guestdata = model.findNodeDataForKey(guestkey);
        positionPersonAtSeat(guestdata);
    }
}

// Position a single guest Node to be at the location of the seat to which they are assigned.
function positionPersonAtSeat(guest) {
    if (guest instanceof go.GraphObject) throw Error("A guest object must not be a GraphObject: " + guest.toString());
    if (!guest || !guest.table || !guest.seat) return;
    var diagram = myDiagram;
    var table = diagram.findPartForKey(guest.table);
    var person = diagram.findPartForData(guest);
    if (table && person) {
        var seat = table.findObject(guest.seat.toString());
        var loc = seat.getDocumentPoint(go.Spot.Center);
        // animate movement, instead of: person.location = loc;
        var animation = new go.Animation();
        animation.add(person, "location", person.location, loc);
        animation.start();
    }
}


// Automatically drag people Nodes along with the table Node at which they are seated.
function SpecialDraggingTool() {
    go.DraggingTool.call(this);
    this.isCopyEnabled = false;  // don't want to copy people except between Diagrams
}
go.Diagram.inherit(SpecialDraggingTool, go.DraggingTool);

SpecialDraggingTool.prototype.computeEffectiveCollection = function (parts) {
    var map = go.DraggingTool.prototype.computeEffectiveCollection.call(this, parts);
    // for each Node representing a table, also drag all of the people seated at that table
    parts.each(function (table) {
        if (isPerson(table)) return;  // ignore persons
        // this is a table Node, find all people Nodes using the same table key
        for (var nit = table.diagram.nodes; nit.next();) {
            var n = nit.value;
            if (isPerson(n) && n.data.table === table.data.key) {
                if (!map.has(n)) map.add(n, new go.DraggingInfo(n.location.copy()));
            }
        }
    });
    return map;
};
// end SpecialDraggingTool
var myGuests_String;
var myDiagram_String;
function save() {
    document.getElementById("Diagram_saved").value = myDiagram.model.toJson();
    document.getElementById("Guests_saved").value = myGuests.model.toJson();
    //document.getElementById("Guests_saved").value = myDiagram.model.toJson();
    myDiagram.isModified = false;
    myGuests.isModified = false;
    myDiagram_String = JSON.stringify(document.getElementById("Diagram_saved").value);
    var hdnfldVariable = document.getElementById('hdnfldVariable');
    hdnfldVariable.value = myDiagram_String;
    myGuests_String = JSON.stringify(document.getElementById("Guests_saved").value)

    var hdnfldVariable2 = document.getElementById('hdnfldVariable2');
    hdnfldVariable2.value = myGuests_String;
}
function load() {

    var jsonGuestFromDatabase = document.getElementById("jsonGuests");
    var jsonTableFromDatabase = document.getElementById("jsonTables");
    myDiagram.model = go.Model.fromJson(JSON.parse(jsonTableFromDatabase.value)); //Do zmiany odczy z bazy
    myGuests.model = go.Model.fromJson(JSON.parse(jsonGuestFromDatabase.value));
   
   
}

function removeFromPalette() {
    myDiagram.commandHandler.deleteSelection();

}
function myCallback(blob) {
    var url = window.URL.createObjectURL(blob);
    var filename = "Zapisany_diagram.svg";

    var a = document.createElement("a");
    a.style = "display: none";
    a.href = url;
    a.download = filename;

    // IE 11
    if (window.navigator.msSaveBlob !== undefined) {
        window.navigator.msSaveBlob(blob, filename);
        return;
    }

    document.body.appendChild(a);
    requestAnimationFrame(function () {
        a.click();
        window.URL.revokeObjectURL(url);
        document.body.removeChild(a);
    });
}

function drukuj() {
    var svg = myDiagram.makeSvg({ scale: 1, background: "white" });
    var svgstr = new XMLSerializer().serializeToString(svg);
    var blob = new Blob([svgstr], { type: "image/svg+xml" });
    myCallback(blob);
}

function addToPalette() {
    if (table_type == 0) {
        myDiagram.startTransaction();
        myDiagram.model.addNodeData({ "key": 1, "category": "TableR3", "name": "Stół 3os.", "guests": {}, "loc": "163.5 58" })
        myDiagram.commitTransaction("added table");

    }
    if (table_type == 1) {
        myDiagram.startTransaction();
        myDiagram.model.addNodeData({ "key": 1, "category": "TableR6", "name":"stół 6os." , "guests": {}, "loc": "163.5 58" })
        myDiagram.commitTransaction("added table");
    }

    if (table_type == 2) {
        myDiagram.startTransaction();
        myDiagram.model.addNodeData({ "key": 1, "category": "TableR5", "name": "stół 5os. ","guests": {}, "loc": "163.5 58" })
        myDiagram.commitTransaction("added table");
    }

    if (table_type == 3) {
        myDiagram.startTransaction();
        myDiagram.model.addNodeData({ "key": 1, "category": "TableR8", "name":"stół 8os." , "guests": {}, "loc": "163.5 58" })
        myDiagram.commitTransaction("added table");
    }

    if (table_type == 4) {
        myDiagram.startTransaction();
        myDiagram.model.addNodeData({ "key": 1, "category": "TableL3_L", "name": "stół 4os.", "guests": {}, "loc": "163.5 58" })
        myDiagram.commitTransaction("added table");
    }
    if (table_type == 5) {
        myDiagram.startTransaction();
        myDiagram.model.addNodeData({ "key": 1, "category": "TableL3_R", "name": "stół 4os.", "guests": {}, "loc": "163.5 58" })
        myDiagram.commitTransaction("added table");
    }
    if (table_type == 6) {
        myDiagram.startTransaction();
        myDiagram.model.addNodeData({ "key": 1, "category": "TableC8", "name": "stół okrągły", "guests": {}, "loc": "163.5 58" })
        myDiagram.commitTransaction("added table");
    }
}

function HorizontalTextRotatingTool() {
    go.RotatingTool.call(this);
}


go.Diagram.inherit(HorizontalTextRotatingTool, go.RotatingTool);

HorizontalTextRotatingTool.prototype.rotate = function (newangle) {
    go.RotatingTool.prototype.rotate.call(this, newangle);
    var node = this.adornedObject.part;
    positionPeopleAtSeats(node);
};




document.addEventListener('DOMContentLoaded', function () {
    var splide = new Splide('#image-slider').mount();
    splide.on('moved', function () {
        table_type = splide.index;
    });
});
