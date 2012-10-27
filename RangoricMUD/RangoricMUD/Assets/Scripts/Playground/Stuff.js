"use strict";
var camera, scene, renderer;
var geometry, material, mesh;

var vMeshes = [];
init();
animate();

function init() {

    camera = new THREE.PerspectiveCamera(75, 500 / 500, 1, 10000);
    camera.position.z = 1000;

    scene = new THREE.Scene();

    geometry = new THREE.CubeGeometry(200, 200, 200);
    material = new THREE.MeshBasicMaterial({ color: 0x0000ff, wireframe: false });
    mesh = new THREE.Mesh(geometry, material);
    mesh.callback = function () { alert("Test"); };
    mesh.position.z -= 500;
    scene.add(mesh);
    vMeshes.push(mesh);
    geometry = new THREE.CubeGeometry(200, 200, 200);
    material = new THREE.MeshBasicMaterial({ color: 0xff0000, wireframe: false });

    mesh = new THREE.Mesh(geometry, material);
    mesh.callback = function () { alert("Test"); };
    scene.add(mesh);
    vMeshes.push(mesh);


    renderer = new THREE.CanvasRenderer();
    renderer.setSize(500, 500);

    document.body.appendChild(renderer.domElement);

}

function animate() {

    // note: three.js includes requestAnimationFrame shim
    requestAnimationFrame(animate);

    vMeshes[0].rotation.x += 0.01;
    vMeshes[0].rotation.y += 0.02;
    vMeshes[1].rotation.x -= 0.02;
    vMeshes[1].rotation.y -= 0.01;

    renderer.render(scene, camera);

}
var projector = new THREE.Projector();
function onDocumentMouseDown(event) {

    event.preventDefault();

    var vector = new THREE.Vector3(
        (event.clientX / 500) * 2 - 1,
        -(event.clientY / 500) * 2 + 1,
        0.5);

    projector.unprojectVector(vector, camera);

    var ray = new THREE.Ray(camera.position, vector.subSelf(camera.position).normalize());

    var intersects = ray.intersectObjects(vMeshes);

    if (intersects.length > 0) {
        var vIndex = 0;
        while(!intersects[vIndex].object.callback && vIndex < intersects.length) {
            vIndex++;
        }
        if(intersects[vIndex].object.callback) {
            intersects[vIndex].object.callback();
        }
    }

}

document.addEventListener('mousedown', onDocumentMouseDown, false);
