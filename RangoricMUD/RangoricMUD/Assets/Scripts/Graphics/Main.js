$(function () {
    var vTarget = $("#Scene");
    var vAspectRatio = vTarget.width() / vTarget.height();
    var vCamera = new THREE.PerspectiveCamera(90, vAspectRatio, 1, 100);
    vCamera.position.z = 30;

    var vGeomtry = new THREE.CubeGeometry(10, 10, 10);
    var vMaterial = new THREE.MeshBasicMaterial({ color: 0x0000ff, wireframe: true });
    var vMesh = new THREE.Mesh(vGeomtry, vMaterial);
    vMesh.position.z = 0;
    vMesh.position.x = 0;
    vMesh.position.y = 0;

    var vScene = new THREE.Scene();

    vScene.add(vMesh);

    var vRenderer = new THREE.CanvasRenderer();
    vRenderer.setSize(vTarget.width(), vTarget.height());

    vTarget.append(vRenderer.domElement);

    var vStats = new Stats();
    vStats.domElement.style.position = 'absolute';
    vStats.domElement.style.bottom = '0';
    vStats.domElement.style.right = '0';

    vTarget.append(vStats.domElement);

    
    function Animate() {
        requestAnimationFrame(Animate);
        vRenderer.render(vScene, vCamera);
        vStats.update();
    }

    Animate();
});