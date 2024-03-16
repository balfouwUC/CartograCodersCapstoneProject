var writingText = false;
var shifted = false;
var landmasses = [[], [], [], [], [], [], [], [], [], []];
var layers = [[0, 1000]];

paper.install(window);

window.onload = function () {
    var canvas = document.getElementById('mainCanvas');

    paper.setup(canvas);

    toolSelect = new Tool();
    toolSelect.activate();
    toolPan = new Tool();
    toolBrush = new Tool();
    toolBrush.fixedDistance = brushSize;
    toolEraser = new Tool();
    toolEraser.fixedDistance = eraserSize;
    toolLine = new Tool();
    toolText = new Tool();
    toolLandmass = new Tool();

    let myPath;
    let pathEnd = new Shape.Circle(new Point(-5, -5), brushSize);
    let text;
    let textBox;
    let cursorCircle = new Shape.Circle(new Point(-5, -5), brushSize);

    toolPan.onMouseDrag = function (event) {
        let pan_offset = event.point.subtract(event.downPoint);
        let newCenter = paper.view.center.subtract(pan_offset);
        if (newCenter.x + (paper.view.size._width / 2) > canvas.width) {
            newCenter.x = canvas.width - (paper.view.size._width / 2);
        }
        else if (newCenter.x - (paper.view.size._width / 2) < 0) {
            newCenter.x = paper.view.size._width / 2;
        }
        if (newCenter.y + (paper.view.size._height / 2) > canvas.height) {
            newCenter.y = canvas.height - (paper.view.size._height / 2);
        }
        else if (newCenter.y - (paper.view.size._height / 2) < 0) {
            newCenter.y = paper.view.size._height / 2;
        }

        paper.view.center = newCenter;
    }

    toolBrush.onMouseDown = function (event) {
        cursorCircle.remove();
        toolBrush.fixedDistance = brushSize;
        let pathStart = new Shape.Circle(event.downPoint, brushSize)
        pathStart.fillColor = brushColor;
        pathStart.strokeColor = brushColor;
        myPath = new Path();
        myPath.fillColor = brushColor;
        myPath.strokeColor = brushColor;
        myPath.add(event.point);
    }
    toolBrush.onMouseDrag = function (event) {
        let step = event.delta;
        step.angle += 90;

        let top = event.middlePoint.add(step);
        let bottom = event.middlePoint.subtract(step);

        let line = new Path();
        line.strokeColor = brushColor;
        line.add(top);
        line.add(bottom);

        myPath.add(top);
        myPath.insert(0, bottom);
        myPath.smooth();

        pathEnd.remove();
        pathEnd = new Shape.Circle(event.point, brushSize);
        pathEnd.fillColor = brushColor;
        pathEnd.strokeColor = brushColor;
    }

    toolBrush.onMouseUp = function (event) {
        AddToLayers(myPath);
    }

    toolBrush.onMouseMove = function (event) {
        cursorCircle.remove();
        cursorCircle = new Shape.Circle(event.point, brushSize)
        cursorCircle.strokeColor = '#808080';
        cursorCircle.strokeWidth = 1;
    }

    toolEraser.onMouseDown = function (event) {
        toolEraser.fixedDistance = eraserSize;
        myPath = new Path();
        myPath.fillColor = backgroundColor;
        myPath.strokeColor = backgroundColor;
        myPath.add(event.point);
    }
    toolEraser.onMouseDrag = function (event) {
        cursorCircle.remove();
        cursorCircle = new Shape.Circle(event.point, eraserSize);
        cursorCircle.strokeColor = 'gray';
        cursorCircle.strokeWidth = 1;

        let step = event.delta;
        step.angle += 90;

        let top = event.middlePoint.add(step);
        let bottom = event.middlePoint.subtract(step);

        let line = new Path();
        line.strokeColor = backgroundColor;
        line.add(top);
        line.add(bottom);

        myPath.add(top);
        myPath.insert(0, bottom);
        myPath.smooth();
    }
    toolEraser.onMouseMove = function (event) {
        cursorCircle.remove();
        cursorCircle = new Shape.Circle(event.point, eraserSize);
        cursorCircle.strokeColor = 'gray';
        cursorCircle.strokeWidth = 1;
    }

    toolLine.onMouseDown = function (event) {
        myPath = new Path();
        myPath.strokeColor = lineColor;
        myPath.strokeWidth = lineSize;
        myPath.add(event.point, event.point);
    }
    toolLine.onMouseDrag = function (event) {
        myPath.lastSegment.point = event.point;
    }

    toolText.onMouseDown = function (event) {
        if (writingText) {
            writingText = false;
            textBox.remove();
        }
        else {
            writingText = true;
            text = new PointText(event.point);
            text.justification = 'center';
            text.fillColor = '@(Model.ColorPalette.TextColor)';
            text.content = '';
            text.fontFamily = textFont;
            text.fontSize = textSize;
            let textRect = new Rectangle(event.point.x - 40, event.point.y - 20, 80, 40);
            textBox = new Path.Rectangle(textRect);
            textBox.strokeColor = 'black';
            textBox.selected = true;
        }
    }
    toolText.onKeyDown = function (event) {
        if (event.key == 'shift')
            shifted = true;

        else if (writingText) {
            if (event.key == 'enter')
                writingText = false;
            else if (event.key == 'backspace')
                text.content = text.content.slice(0, -1);
            else {
                if (shifted)
                    text.content = text.content + event.key.replace('space', ' ').toUpperCase();
                else
                    text.content = text.content + event.key.replace('space', ' ');
            }
        }
    }
    toolText.onKeyUp = function (event) {
        if (event.key == 'shift')
            shifted = false;
    }

    toolLandmass.onMouseDown = function (event) {
        cursorCircle.remove();
        myPath = new Path();
        myPath.strokeColor = brushColor;
        myPath.add(event.point);
    }
    toolLandmass.onMouseDrag = function (event) {
        myPath.add(event.point);
    }
    toolLandmass.onMouseUp = function (event) {
        myPath.smooth();
        myPath.fillColor = brushColor;
        AddToLayers(myPath);
    }
    toolLandmass.onMouseMove = function (event) {
        cursorCircle.remove();
        cursorCircle = new Shape.Circle(event.point, 3)
        cursorCircle.strokeColor = '#808080';
        cursorCircle.strokeWidth = 1;
    }
}

function AddToLayers(path) {
    let layerButtons = document.getElementsByClassName('layer-button');
    for (let i = 0; i < layerButtons.length; i++) {
        if (layerButtons[i].classList.contains('selected'))
            landmasses[i].push(path);
    }
}