# 🔐 Criptografia de Hill

## O que é Criptografia?
A criptografia estuda métodos para codificar uma mensagem de modo que somente 
seu destinatário legítimo consiga interpretá-la.
O sistema de hill foi amplamente utilizado desde sua origem até meados da **Segunda Guerra 
Mundial (1939-1945)**, 

## 🧮 Como funciona?
A Cifra de Hill é um sistema de **criptografia de chave privada**, ou seja, 
a chave de codificação deve ser mantida em sigilo entre o remetente e o destinatário.

O método utiliza conceitos de:
- **Álgebra Linear** — operações com matrizes, independência 
linear, base e transformações matriciais
- **Aritmética Modular** — congruência e inverso modular

### Passo a passo:
1. A palavra é convertida em índices numéricos (A=1, B=2... Z=26)
2. Os índices são organizados em uma matriz **M**
3. O usuário define uma matriz chave **C**
4. A multiplicação **C × M** gera a matriz criptografada **M'**
5. Para descriptografar, calcula-se a inversa de **C** e multiplica por **M'**

<img width="1536" height="1024" alt="criptografia_passo_a_passo" src="https://github.com/user-attachments/assets/6916d0ad-4f40-4af8-a4be-7e45d0d8df35" />

## Sobre o projeto

Implementação da Cifra de Hill em **C#**, desenvolvida como estudo prático dos 
conceitos de Álgebra Linear e Aritmética Modular aplicados à criptografia.

### Funcionalidades
- Validação de entrada (aceita apenas letras)
- Conversão de letras em índices numéricos
- Organização automática em matriz quadrada (2x2 ou 3x3)
- Preenchimento com `*` nas células vazias
- Criptografia via multiplicação matricial
- Descriptografia via matriz inversa da chave
- Validação automática da matriz chave
- Menu com orientações sobre como funciona e como escolher uma chave válida
- Exibe palavra criptografada em números e letras

 ### 📁 Estrutura em classes do Projeto (versão POO)
Validacao: Validação de strings e inteiros
Matrizes: Operações com matrizes
Operações: Criptografia e descriptografia
TelaMenu: Menu e telas de orientação
Program: Ponto de entrada e fluxo principal

### 🌿 Branches
main: Versão sem POO(procedural) — funções no Program.cs

refactor/poo: Versão refatorada com classes

## 📚 Status
✅ `main` — versão procedural concluída  
✅ `refactor/poo` — versão orientada a objetos concluída
