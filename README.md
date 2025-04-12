# 🔍 OCR Search API

API ASP.NET Core para upload de documentos (PDFs e imagens), extração de texto via OCR e indexação no Elasticsearch, permitindo busca full-text rápida e eficiente.

---

## 📚 Visão Geral

Este projeto tem como objetivo criar um sistema de **busca inteligente em documentos** por meio de:
- Upload de arquivos (PDF, PNG, JPG)
- Extração de texto com OCR (Tesseract)
- Indexação de conteúdo e metadados no Elasticsearch
- API de busca com filtros e relevância

---

## 🚀 Tecnologias Utilizadas

| Camada | Tecnologia |
|--------|------------|
| Backend | ASP.NET Core Web API |
| OCR | Tesseract OCR |
| Busca | Elasticsearch + NEST |
| Armazenamento (opcional) | PostgreSQL / MongoDB |
| Containerização | Docker + Docker Compose |
| Outros | Serilog, Swagger, AutoMapper, FluentValidation |

---

## 📦 Funcionalidades

- [x] Upload de arquivos PDF e imagens
- [x] Extração de texto com OCR
- [x] Indexação no Elasticsearch
- [x] API REST para busca textual
- [ ] Highlight de termos encontrados
- [ ] Filtros por tipo de documento, data, etc.
- [ ] Autenticação com JWT (futuro)
- [ ] Dashboard de monitoramento (futuro)

---

## 📁 Estrutura do Projeto

```bash
/src
  /Application       # Lógica de negócio
  /Domain            # Entidades e contratos
  /Infrastructure    # OCR, Elasticsearch, repositórios
  /WebAPI            # Controllers e configuração
  /OCR               # Integração com Tesseract
/docker
  docker-compose.yml # Elasticsearch + Kibana
```

---

## 🛠️ Setup do Projeto

### 🔧 Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [Docker](https://www.docker.com/)
- [Elasticsearch](https://www.elastic.co/elasticsearch/) (v8.x recomendado)

### 🚨 Subindo o Elasticsearch com Docker

```bash
cd docker
docker-compose up -d
```

Acesse o Kibana: [http://localhost:5601](http://localhost:5601)

### ▶️ Rodando a API

```bash
cd src/WebAPI
dotnet run
```

Swagger disponível em: [http://localhost:5000/swagger](http://localhost:5000/swagger)

---

## 🔐 Endpoints Principais

### 📤 Upload de Documento

```http
POST /api/documents/upload
Content-Type: multipart/form-data
```

**Body:**
- Arquivo: PDF ou imagem

---

### 🔍 Buscar Documento

```http
GET /api/search?q=contrato+fornecedor
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

## 🧠 Considerações Futuras

- Integração com Azure Blob Storage ou AWS S3
- OCR assíncrono com filas (ex: RabbitMQ)
- Upload em lote de arquivos
- Detecção automática de idioma
- Interface web para upload e busca (Blazor, Vue ou React)

---

## 📄 Licença

Projeto open-source sob a licença MIT. Sinta-se à vontade para usar, modificar e contribuir!
