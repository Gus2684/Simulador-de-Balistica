# Simulador Balístico - Evaluación de Físicas en Unity

Simulador interactivo construido en Unity que permite configurar parámetros balísticos (ángulo, fuerza y masa) para derribar estructuras mediante cálculos físicos de Rigidbodies y colisiones.

## 🛠 Entorno de Desarrollo
* **Motor:** Unity [3.21.2]
* **Lenguaje:** C#
* **Físicas:** Sistema nativo 3D (Rigidbodies, Colliders, Joints)

## 🎮 Controles y Cómo Jugar
La interfaz en pantalla ("Canvas") proporciona todos los controles necesarios para configurar el disparo:
1. **Slider Horizontal:** Rota la base del cañón (Apunte en Y).
2. **Slider Vertical:** Ajusta la elevación del barril (Apunte en X).
3. **Slider de Fuerza:** Define el multiplicador de impulso físico (`ForceMode.Impulse`) aplicado al proyectil.
4. **Slider de Masa:** Ajusta el peso (`rb.mass`) del proyectil antes de ser disparado. **Feedback visual:** El tamaño de la bala escala de manera proporcional a la masa elegida.
5. **Botón "Disparar":** Instancia el proyectil, aplica los cálculos físicos y la cámara pasa a modo seguimiento.
6. **Botón "Reiniciar":** Recarga la escena para volver a intentar el tiro desde cero.

