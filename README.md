# 🔍 OCR Search API

API para upload de documentos (PDFs e imagens), extração de texto via OCR e indexação no Elasticsearch, permitindo busca full-text rápida e eficiente.

---

## 📚 Visão Geral

Este projeto visa criar um sistema de **busca inteligente em documentos** por meio de:
- Upload de arquivos (PDF, PNG, JPG)
- Extração de texto com OCR (Tesseract)
- Indexação de conteúdo e metadados no Elasticsearch
- API de busca com filtros e relevância

---

## 🚀 Tecnologias Utilizadas

| Camada | Tecnologia                                    |
|--------|-----------------------------------------------|
| Backend | ASP.NET Core Web API                          |
| OCR | Tesseract OCR                                 |
| Busca | Elasticsearch + NEST                          |
| Containerização | Docker + Docker Compose                       |
| Outros | Serilog, Scalar, AutoMapper, FluentValidation |

---

## 📦 Funcionalidades

- [X] Upload de arquivos PDF e imagens
- [ ] Extração de texto com OCR
- [ ] Indexação no Elasticsearch
- [ ] API REST para busca textual
- [ ] Highlight de termos encontrados
- [ ] Filtros por tipo de documento, data, etc.

---

## 📁 Estrutura do Projeto

```bash
/OCRSearch.Application       # Lógica de negócio
/OCRSearch.Domain            # Entidades e contratos
/OCRSearch.Infrastructure    # OCR, Elasticsearch, repositórios
/OCRSearch.API               # Controllers e configuração
/OCRSearch.OCR               # Integração com Tesseract
docker-compose.yml # Elasticsearch + Kibana
```

---

## 🛠️ Setup do Projeto

### 🔧 Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download)
- [Docker](https://www.docker.com/)
- [Elasticsearch](https://www.elastic.co/elasticsearch/) (v8.x recomendado)

### 🚨 Subindo o Elasticsearch com Docker

```bash
docker-compose up --build -d
```

Acesse o Kibana: [http://localhost:5601](http://localhost:5601)

### ▶️ Rodando a API

```bash
cd OCRSearch.Api/
dotnet run
```

Swagger disponível em: [http://localhost:5174/scalar/v1](http://localhost:5000/swagger)

---

## 🔐 Endpoints Principais

### 📤 Upload de Documento

```http
POST /upload-file
Content-Type: multipart/form-data
```

**Body:**
- Arquivo: PDF ou imagem

---

### 🔍 Buscar Documento

```http
GET /get-file/search?q=contrato+fornecedor
```

**Query Params:**
- `q`: Termo de busca

**Retorno:**
```json
[
  {
    "id": "abc123",
    "title": "Contrato_Fornecedor.pdf",
    "snippet": "...encontrado no contrato firmado com o fornecedor..."
  }
]
```

---

## 📄 Licença

Projeto open-source sob a licença MIT. Sinta-se à vontade para usar, modificar e contribuir!
