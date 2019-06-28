<template>
    <div>
        <div class="greeting">Hello {{name}}{{exclamationMarks}}</div>

        <v-btn fab dark small color="primary" @click="decrement">
            <v-icon dark>thumb_down</v-icon>
        </v-btn>

        <v-btn fab dark small color="primary" @click="increment">
            <v-icon dark>thumb_up</v-icon>
        </v-btn>
    </div>
</template>
<script src="./Hello.ts" lang="ts"></script>
<style src="./Hello.css"></style>