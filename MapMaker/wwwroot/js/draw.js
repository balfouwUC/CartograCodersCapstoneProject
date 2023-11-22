var canvas = document.getElementById('mainCanvas');

window.onload = function () {
    paper.setup(canvas);
    paper.view.draw();
}

let myPath;

function onMouseDown(event) {
    myPath = new Path();
    myPath.strokeColor = 'black';
}

function onMouseDrag(event) {
    myPath.add(event.point);
}

canvas.addEventListener('mousedown', (e) => onMouseDown(e));
canvas.addEventListener('mousemove', (e) => onMouseDrag(e));