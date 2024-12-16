<template>
  <div class="app-container">
    <h1 class="app-title">The Guardian News</h1>
    <div class="card search-card">
      <h2>Search</h2>
      <div class="form-row-search">
        <div class="search-container">
          <input
            v-model="searchQuery"
            type="text"
            placeholder="Add the subject you want to search"
            class="search-bar"
          />
          <button @click="fetchNews" class="search-button">Search</button>
        </div>
      </div>
    </div>
    <div class="card filter-card">
      <h2>Filters</h2>
      <div class="form-row">
        <div class="form-group">
          <input v-model="category" type="text" placeholder="Category" />
        </div>
        <div class="form-group">
          <VueDatePicker
            v-model="fromDate"
            placeholder="From Date"
            :format="dateFormat"
          />
        </div>
        <div class="form-group">
          <VueDatePicker
            v-model="toDate"
            placeholder="To Date"
            :format="dateFormat"
          />
        </div>
        <div class="form-group">
          <input v-model="subject" type="text" placeholder="Search" />
        </div>
      </div>
    </div>

    <div v-if="loading" class="loading">Loading News...</div>

    <div v-else-if="filteredNews.length > 0" class="news-grid">
      <div v-for="(item, index) in filteredNews" :key="index" class="news-item">
        <h4>{{ item.title }}</h4>
        <p><strong>Category:</strong> {{ item.category }}</p>
        <p><strong>Published:</strong> {{ formatDate(item.date) }}</p>
        <a :href="item.link" target="_blank" class="news-link">Read More</a>
      </div>
    </div>
    <div v-else class="no-news">
      <p>No news found.</p>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref, computed } from "vue";
import { useStore } from "vuex";
import VueDatePicker from "@vuepic/vue-datepicker";
import "@vuepic/vue-datepicker/dist/main.css";

export default defineComponent({
  name: "NewsSearch",
  components: {
    VueDatePicker,
  },
  setup() {
    const store = useStore();
    const loading = ref(true);

    const dateFormat = (date: Date) => {
      return date ? date.toISOString().split("T")[0] : "";
    };

    const formatDate = (dateString: string) => {
      return new Date(dateString).toLocaleDateString("en-US", {
        year: "numeric",
        month: "long",
        day: "numeric",
      });
    };

    const category = ref("");
    const fromDate = ref<Date | null>(null);
    const toDate = ref<Date | null>(null);
    const subject = ref("");
    const searchQuery = ref("");

    const news = computed(() => store.state.news);

    const fetchAllNews = async () => {
      try {
        loading.value = true;
        await store.dispatch("getAllNews");
      } catch (error) {
        console.error(error);
      } finally {
        loading.value = false;
      }
    };

    const fetchNews = async () => {
      try {
        loading.value = true;
        await store.dispatch("addSearchParams", searchQuery.value);

        const newNews = await store.dispatch(
          "fetchNewsBySearchInput",
          searchQuery.value
        );

        store.commit("addNews", newNews);

        for (const newsItem of newNews) {
          await store.dispatch("addNewsToDb", newsItem);
        }

        await fetchAllNews();
      } catch (error) {
        console.error(error);
      } finally {
        loading.value = false;
      }
    };

    const filteredNews = computed(() => {
      return news.value.filter((item: any) => {
        const matchesCategory =
          !category.value ||
          item.category.toLowerCase().includes(category.value.toLowerCase());

        const matchesFromDate =
          !fromDate.value || new Date(item.date) >= fromDate.value;

        const matchesToDate =
          !toDate.value || new Date(item.date) <= toDate.value;

        const matchesSubject =
          !subject.value ||
          item.title.toLowerCase().includes(subject.value.toLowerCase());

        return (
          matchesCategory && matchesFromDate && matchesToDate && matchesSubject
        );
      });
    });

    onMounted(fetchAllNews);

    return {
      category,
      fromDate,
      toDate,
      subject,
      searchQuery,
      news,
      filteredNews,
      fetchNews,
      loading,
      dateFormat,
      formatDate,
    };
  },
});
</script>

<style scoped>
.app-container {
  font-family: Arial, sans-serif;
  margin: 0;
  padding: 20px;
  background-color: #f4f4f9;
  min-height: 100vh;
}

.app-title {
  text-align: center;
  margin-bottom: 20px;
  font-size: 2em;
  color: #333;
}

.card {
  background: #fff;
  padding: 20px;
  border-radius: 8px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  margin-bottom: 20px;
}

.form-row {
  display: flex;
  flex-wrap: wrap;
  gap: 15px;
  align-items: center;
}

.form-row-search {
  flex-wrap: wrap;
  gap: 15px;
  align-items: center;
}

.form-group {
  flex: 1;
  display: flex;
  flex-direction: column;
}

.search-button-group {
  display: flex;
  align-items: flex-start;
  justify-content: flex-start;
}

.search-button {
  padding: 10px 20px;
  font-size: 14px;
  background-color: #007bff;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  display: flex;
  align-items: center;
}

.search-button:hover {
  background-color: #0056b3;
}

.search-container {
  display: flex;
  align-items: center;
}

.search-bar {
  flex: 0 0 90%;
  margin-right: 10px; /* Optional: Adds some space between the input and the button */
}

.form-group input,
.search-container input {
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 4px;
  font-size: 14px;
}

.loading {
  text-align: center;
  padding: 20px;
  font-size: 1.2em;
  color: #666;
}

.news-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
  gap: 16px;
  padding: 16px;
}

.news-item {
  border: 1px solid #e0e0e0;
  border-radius: 8px;
  padding: 16px;
  background-color: #f9f9f9;
  transition: box-shadow 0.3s ease;
}

.news-item:hover {
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

.news-item h2 {
  margin-top: 0;
  font-size: 1.2em;
  color: #333;
}

.news-link {
  display: inline-block;
  margin-top: 10px;
  color: #007bff;
  text-decoration: none;
}

.news-link:hover {
  text-decoration: underline;
}

.no-news {
  text-align: center;
  padding: 20px;
  font-size: 1.2em;
  color: #666;
}
</style>
